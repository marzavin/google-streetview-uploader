using System;

namespace AM.Google.StreetView.Core
{
    internal class PhotoMetadataResult : OperationResult
    {
        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public DateTime? DateTime { get; set; }
    }
}
