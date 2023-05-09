using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelsoft.Log.Common.Data
{
    public class DBManager
    {
        /// <summary>
        /// Ejecuta una consulta y devuelve un resultado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pConnString"></param>
        /// <param name="pQueryFunction"></param>
        /// <param name="pErrorMessage"></param>
        /// <returns></returns>
        public static T ExecuteStoreProcedure<T>(string pConnString, Func<SqlConnection, SqlCommand, T> pQueryFunction, Action<Exception> pManageErrorFunction, bool pTransactional = false)
        {
            SqlConnection wCnn = null;
            SqlTransaction wTransaction = null;
            T wResult = default(T);

            try
            {
                wCnn = new SqlConnection(pConnString);
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (pTransactional)
                        wTransaction = BeginTransaction(wCnn);

                    cmd.Connection = wCnn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    wCnn.Open();

                    wResult = pQueryFunction(wCnn, cmd);

                    if (wTransaction != null)
                        wTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction(wTransaction);
                pManageErrorFunction(ex);
            }
            finally
            {
                CloseConnection(wCnn, wTransaction);
            }

            return wResult;
        }

        /// <summary>
        /// Ejecuta una consulta pero no devuelve resultados
        /// </summary>
        /// <param name="pConnString"></param>
        /// <param name="pQueryFunction"></param>
        /// <param name="pErrorMessage"></param>
        public static void ExecuteStoreProcedure(string pConnString, Action<SqlConnection, SqlCommand> pQueryFunction, Action<Exception> pManageErrorFunction, bool pTransactional = false)
        {
            SqlConnection wCnn = null;
            SqlTransaction wTransaction = null;

            try
            {
                wCnn = new SqlConnection(pConnString);
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (pTransactional)
                        wTransaction = BeginTransaction(wCnn);

                    cmd.Connection = wCnn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    wCnn.Open();
                    pQueryFunction(wCnn, cmd);

                    if (wTransaction != null)
                        wTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction(wTransaction);
                pManageErrorFunction(ex);
            }
            finally
            {
                CloseConnection(wCnn, wTransaction);
            }
        }

        private static SqlTransaction BeginTransaction(SqlConnection pCnn)
        {
            string wID = string.Empty;
            if (pCnn == null)
                return null;

            //Genero un ID de transacción
            wID = string.Format("tr_{0}", Guid.NewGuid().ToString().Replace("-", string.Empty));
            return pCnn.BeginTransaction(IsolationLevel.ReadUncommitted, wID);
        }

        private static void CloseConnection(SqlConnection pCnn, SqlTransaction pTransaction)
        {
            if (pCnn != null && pCnn.State != ConnectionState.Closed)
            {
                pCnn.Close();
                pCnn.Dispose();
                pCnn = null;
            }

            if (pTransaction != null)
            {
                pTransaction.Dispose();
                pTransaction = null;
            }
        }

        /// <summary>
        /// Hace rollback de una transacción
        /// </summary>
        /// <param name="pTransaction"></param>
        private static void RollbackTransaction(SqlTransaction pTransaction)
        {
            if (pTransaction == null)
                return;

            try
            {
                pTransaction.Rollback();
            }
            catch { }
        }
    }
}
