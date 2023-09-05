namespace ChronoDatabase.Models
{
    /// <summary>
    /// Time Tracker Activity Info class
    /// </summary>
    public class ChronoActivityInfo : ChronoBaseActivityInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets activity type identifier
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets activity title
        /// </summary>
        public string ActivityTitle { get; set; }

        /// <summary>
        /// Gets or sets sub activity title
        /// </summary>
        public string SubActivityTitle { get; set; }

        /// <summary>
        /// Gets or sets activity start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets activity end time
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets location identifier
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets location info
        /// </summary>
        public ChronoLocationInfo LocationInfo { get; set;}
        
        /// <summary>
        /// Gets or sets application id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets audit identifier
        /// </summary>
        public int? AuditId { get; set; }

        /// <summary>
        /// Gets or sets store identifier
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// Gets or sets store code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Gets or sets is manual activity property
        /// </summary>
        public bool IsManualActivity { get; set; }

        /// <summary>
        /// Gets or sets is exported property
        /// </summary>
        public bool IsExported { get; set; }

        /// <summary>
        /// Gets or sets the flag indicates confliciting activities and unites conflicting activities by some unique value
        /// </summary>
        public int? Flag { get; set; }
        #endregion

        /// <summary>
        /// Check Activity info identical by TypeId, ActivityTitle, LocationId, LocationInfo and AuditId
        /// </summary>
        /// <param name="activityInfo">Activity to compare</param>
        /// <returns>true - info is identical, false - in another cases</returns>
        public bool InfoEquals(ChronoActivityInfo activityInfo)
        {
            return TypeId == activityInfo.TypeId &&
                ((!string.IsNullOrEmpty(ActivityTitle) && ActivityTitle.Equals(activityInfo.ActivityTitle)) ||
                    (string.IsNullOrEmpty(ActivityTitle) && string.IsNullOrEmpty(activityInfo.ActivityTitle))) &&
                LocationId == activityInfo.LocationId && 
                ((LocationInfo != null && LocationInfo.Equals(activityInfo.LocationInfo)) ||
                (LocationInfo == null && activityInfo.LocationInfo == null)) &&
                AuditId == activityInfo.AuditId &&
                ((!string.IsNullOrEmpty(StoreId) && StoreId.Equals(activityInfo.StoreId)) ||
                    (string.IsNullOrEmpty(StoreId) && string.IsNullOrEmpty(activityInfo.StoreId))) && 
                ((!string.IsNullOrEmpty(StoreCode) && StoreCode.Equals(activityInfo.StoreCode)) ||
                    (string.IsNullOrEmpty(StoreCode) && string.IsNullOrEmpty(activityInfo.StoreCode)));

        }
    }
}

