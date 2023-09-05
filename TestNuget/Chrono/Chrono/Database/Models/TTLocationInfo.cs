using Chrono;
using Chrono.Constants;
using System.Linq;

namespace ChronoDatabase.Models
{
    public class ChronoLocationInfo
    {
        public int OptionId { get; set; }

        public string Name { get; set; }

        public ChronoAuditInfo AuditInfo { get; set; }

        public string Key { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationInfo"/> class.
        /// </summary>
        /// <param name="optionId">Option identifier.</param>
        /// <param name="name">Location name.</param>
        /// <param name="auditInfo">Store info.</param>
        /// <param name="key">Key.</param>
        public ChronoLocationInfo(int optionId, string name, ChronoAuditInfo auditInfo, string key)
        {
            OptionId = optionId;
            Name = name;
            AuditInfo = auditInfo;
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationInfo"/> class.
        /// </summary>
        /// <param name="optionId">Option identifier.</param>
        /// <param name="locationName">Location name.</param>
        public ChronoLocationInfo(int optionId, string locationName)
        {
            OptionId = optionId;
            Name = locationName;
            Key = optionId.ToString();
        }

        /// <summary>TT
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="TimeTrackerHelper+LocationInfo"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="TimeTrackerHelper+LocationInfo"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="TimeTrackerHelper+LocationInfo"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return (obj is ChronoLocationInfo locationObj) && locationObj.Key.Equals(Key);
        }

        /// <summary>
        /// To be added.
        /// </summary>
        /// <returns>To be added.</returns>
        /// <remarks>To be added.</remarks>
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}

