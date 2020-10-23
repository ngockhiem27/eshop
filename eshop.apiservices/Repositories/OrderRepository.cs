using Dapper;
using Dapper.Oracle;
using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.apiservices.Repositories
{
    public class OrderRepository : BaseRepositoty, IOrdersRepository
    {
        private const string ORDER_STORED_PACKAGE = "ESHOP_ORDERS_API.";

        public OrderRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<OrdersViewModel> AddOrder(int customerId, List<OrderItems> items)
        {
            var conn = GetOpenConnection();
            using var trans = conn.BeginTransaction();
            try
            {
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_ADDORDERS";
                var totalPrice = items.Select(i => i.Item_Price * i.Quantity).Sum();
                var param = new OracleDynamicParameters();
                param.Add(name: "ORD_CUSTOMER_ID", customerId, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                param.Add(name: "ORD_TOTAL", totalPrice, dbType: OracleMappingType.Decimal, direction: ParameterDirection.Input);
                param.Add(name: "ORD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var order = (await conn.QueryAsync<OrdersViewModel>(query, param: param, transaction: trans, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                if (order == null)
                {
                    trans.Rollback();
                    return null;
                }
                var effectedRow = await AddOrderItems(order.Id, items, conn, trans);
                if (effectedRow != -items.Count)
                {
                    trans.Rollback();
                    return null;
                }
                else
                {
                    trans.Commit();
                    return order;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        public async Task<List<OrdersViewModel>> GetAllOrders()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("ORD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_GETALLORDERS";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<OrdersViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrdersViewModel>> GetAllCustomerOrders(int customerId)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("ORD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                param.Add("ORD_CUSTOMER_ID", customerId, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_GETALLCUSTOMERORDERS";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<OrdersViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderItemsViewModel>> GetAllOrderItems()
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("OIT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_GETALLORDERITEMS";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<OrderItemsViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrdersViewModel> GetOrderById(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("ORD_ID", dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                param.Add("ORD_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_GETORDERBYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<OrdersViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OrderItemsViewModel> GetOrderItemsById(int id)
        {
            try
            {
                var param = new OracleDynamicParameters();
                param.Add("OIT_ID", dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                param.Add("OIT_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_GETORDERITEMSBYID";
                var conn = GetOpenConnection();

                var result = (await SqlMapper.QueryAsync<OrderItemsViewModel>(conn, query, param: param, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<int> AddOrderItems(int orderId, List<OrderItems> items, IDbConnection conn, IDbTransaction trans)
        {
            try
            {
                var paramList = new List<object>();
                var query = ORDER_STORED_PACKAGE + "SP_ORDERS_ADDORDERITEMS";
                items.ForEach(i =>
                {
                    var param = new OracleDynamicParameters();
                    param.Add(name: "OIT_PRODUCT_ID", i.Product_Id, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                    param.Add(name: "OIT_ORDER_ID", orderId, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                    param.Add(name: "OIT_QUANTITY", i.Quantity, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                    param.Add(name: "OIT_ITEM_PRICE", i.Item_Price, dbType: OracleMappingType.Decimal, direction: ParameterDirection.Input);

                    paramList.Add(param);
                });
                var effectedRow = await conn.ExecuteAsync(query, param: paramList, transaction: trans, commandType: CommandType.StoredProcedure);
                return effectedRow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
