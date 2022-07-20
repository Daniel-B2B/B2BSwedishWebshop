using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.GroupBuy.Domain;
using Nop.Plugin.Misc.GroupBuy.Models;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupBuy.Services
{
    public class GroupBuyService : IGroupBuyService
    {
        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<GroupBuyProduct> _productSellRepository;
        private readonly IRepository<CustomTierPriceModel> _tierPriceRepository;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPriceFormatter _priceFormatter;

        #endregion


        #region Ctor
        public GroupBuyService( IRepository<Product> productRepository, 
            IRepository<GroupBuyProduct> productSellRepository,
            IRepository<CustomTierPriceModel> tierPriceRepository,
            IOrderService orderService,
            IProductService productService,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Order> orderRepository,
            IDateTimeHelper dateTimeHelper,
            IPriceFormatter priceFormatter
            )
        {
            this._productRepository = productRepository;
            this._productSellRepository = productSellRepository;
            this._tierPriceRepository = tierPriceRepository;
            this._orderService = orderService;
            this._productService = productService;
            this._orderItemRepository = orderItemRepository;
            this._orderRepository = orderRepository;
            this._dateTimeHelper = dateTimeHelper;
            this._priceFormatter = priceFormatter;
            
        }

        #endregion

        #region Methods
       

        public bool CreateProductQuantity(GroupBuyProduct productSellModel)
        {
            _productSellRepository.Insert(productSellModel);
            return true;
            
        }

        public GroupBuyProduct GetProductSellQuantityDetails(int id)
        {
            var productSellModel = _productSellRepository.GetById(id);
            return productSellModel;
        }

        public void UpdateProductQunatityAndDates(GroupBuyProduct productSellModel)
        {       
            var existingModel = _productSellRepository.GetById(productSellModel.Id);
            if(existingModel != null)
            {                
                existingModel.StartDate = productSellModel.StartDate;
                existingModel.EndDate = productSellModel.EndDate;
                existingModel.Sku = productSellModel.Sku;
                existingModel.ProductId = productSellModel.ProductId;
            }

            _productSellRepository.Update(existingModel);
        }

        public void DeleteProductDetails(int id)
        {
           var productSell = _productSellRepository.GetById(id);
            _productSellRepository.Delete(productSell);

             var productTierPriceList = _tierPriceRepository.Table.Where(x => x.ProductSellId == id).ToList();
            if (productTierPriceList.Count != 0)
            {
                foreach (var item in productTierPriceList)
                {
                    _tierPriceRepository.Delete(item);
                }
            }
        }

        #region Tier Price

        public IPagedList<CustomTierPriceModel> GetAllTierPriceById(int id, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            
             var model = _tierPriceRepository.Table.Where(t => t.ProductSellId == id).OrderBy(t => t.MaxQuantity).ToList();

            return new PagedList<CustomTierPriceModel>(model, pageIndex, pageSize);
        }

        public void CreateTierPrice(CustomTierPriceModel customTierPrice)
        {
            _tierPriceRepository.Insert(customTierPrice);
        }

        public void UpdateTierPrice(CustomTierPriceModel customTierPrice)
        {

            _tierPriceRepository.Update(customTierPrice);
        }

        public void DeleteTierPrice(int id)
        {
            var entity = _tierPriceRepository.GetById(id);
            _tierPriceRepository.Delete(entity);
        }




        public IPagedList<GroupBuyProduct> GetAllGroupBuyProductList(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _productSellRepository.Table;
            var items = query.ToList();
            foreach(var item in items)
            {
                var orderedQunatity = _orderItemRepository.Table.Join(_orderRepository.Table, ot => ot.OrderId, o => o.Id, (ot, o) => new { ot, o }).Where(ot => ot.ot.ProductId == item.ProductId && ot.o.PaymentStatusId == 30).ToList().Sum(ot => ot.ot.Quantity);
                item.ProductSoldQuantity = orderedQunatity;
                item.ProductName = _productService.GetProductById(item.ProductId).Name;
                if (item.ProductName.Length > 33)
                    item.ProductName = item.ProductName.Substring(0, 30) + "...";


            }
            return new PagedList<GroupBuyProduct>(items, pageIndex, pageSize);
        }

        public CustomTierPriceModel GetTierPriceById(int id)
        {
            var tierPrice = _tierPriceRepository.GetById(id);
            return tierPrice;
        }


        public CustomTierPriceModel GetCustomTierPriceModel(int groupBuyId)
        {
            CustomTierPriceModel model = new CustomTierPriceModel();
            model.ProductSellId = groupBuyId;
            model.ProductName = _productService.GetProductById(_productSellRepository.GetById(groupBuyId).ProductId).Name;
            return model;
        }

        #endregion

        #endregion

        #region Front End Method
        public FrontEndViewModel GetTIerPriceByProductId(int productId)
        {

            var groupBuyProduct = _productSellRepository.Table.Where(p => p.ProductId == productId && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now).FirstOrDefault(); 
            if (groupBuyProduct != null)
            {

                DateTime? startDateValue = (groupBuyProduct.StartDate == null) ? null
                               : (DateTime?)_dateTimeHelper.ConvertToUtcTime(Convert.ToDateTime(groupBuyProduct.StartDate), _dateTimeHelper.CurrentTimeZone);

                DateTime? endDateValue = (groupBuyProduct.EndDate == null) ? null
                                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(Convert.ToDateTime(groupBuyProduct.EndDate), _dateTimeHelper.CurrentTimeZone).AddDays(1);

                int productSoldQuantity = 0;
                var orders = _orderService.SearchOrders(productId: groupBuyProduct.ProductId, createdFromUtc: startDateValue, createdToUtc: endDateValue).Where(x => x.PaymentStatusId == 30).ToList();
                if (orders.Count > 0)
                {
                    productSoldQuantity = orders.FirstOrDefault().OrderItems.Where(p => p.ProductId == groupBuyProduct.ProductId).Sum(p => p.Quantity);
                }



                FrontEndViewModel frontEndViewModel = new FrontEndViewModel();
                if (groupBuyProduct != null)
                {
                    var customTierPriceList = _tierPriceRepository.Table.Where(p => p.ProductId == groupBuyProduct.ProductId).GroupBy(x => x.MaxQuantity).ToList();

                    foreach (var tierPrice in customTierPriceList)
                    {
                        foreach (var item in tierPrice)
                        {
                            item.PriceString = _priceFormatter.FormatPrice(item.Price.Value);
                        }

                    }

                    //frontEndViewModel.CustomTierPriceModelList = _tierPriceRepository.Table.Where(p => p.ProductId == groupBuyProduct.ProductId).GroupBy(x => x.MaxQuantity).ToList();
                    frontEndViewModel.CustomTierPriceModelList = customTierPriceList;
                    frontEndViewModel.SoldQuantity = productSoldQuantity;
                }
                return frontEndViewModel;
            }

            return null;

        }
        #endregion
    }
}
