namespace Chrono
{
    /// <summary>
    /// Class for keeping Audit info
    /// </summary>
    public class ChronoAuditInfo
    {
        /// <summary>
        /// Gets Audit Id
        /// </summary>
        public int AuditId { get; private set; }

        /// <summary>
        /// Gets Store Id
        /// </summary>
        public string StoreId    { get; private set; }

        /// <summary>
        /// Gets Store Code
        /// </summary>
        public string StoreCode { get; private set; }

        /// <summary>
        /// Gets App Id
        /// </summary>
        public string AppId { get; private set; }


        /// <summary>
        /// Initializes the instance of <see cref="ChronoAuditInfo.cs"/>
        /// </summary>
        /// <param name="auditId">Audit Id</param>
        /// <param name="storeId">Store Id</param>
        /// <param name="storeCode">Store Code</param>
        /// <param name="appId">App Id</param>
        public ChronoAuditInfo(int auditId, string storeId, string storeCode, string appId)
        {
            AuditId = auditId;
            StoreId = storeId;
            StoreCode = storeCode;
            AppId = appId;
        }
    }
}