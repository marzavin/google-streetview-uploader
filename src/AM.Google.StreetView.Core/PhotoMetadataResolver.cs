using CoordinateSharp;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Google.StreetView.Core
{
    internal class PhotoMetadataResolver
    {
        public Task<PhotoMetadataResult> GetMetadataAsync(string filePath)
        {
            var result = new PhotoMetadataResult();

            IReadOnlyList<Directory> directories = ImageMetadataReader.ReadMetadata(filePath);

            var ifd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
            if (ifd0 == null)
            {
                result.Errors.Add("Directory 'IFD0' is not found in the metadata.");
            }
            else
            {
                if (ifd0.ContainsTag(ExifDirectoryBase.TagDateTime))
                {
                    result.DateTime = ifd0.GetDateTime(ExifDirectoryBase.TagDateTime);
                }
                else
                {
                    result.Errors.Add("Tag 'Date/Time' is not found in the 'IFD0' directory.");
                }
            }

            var gps = directories.OfType<GpsDirectory>().FirstOrDefault();
            if (gps == null)
            {
                result.Errors.Add("'GPS' directory is not found in the metadata.");
            }
            else
            {
                var builder = new StringBuilder();

                if (gps.ContainsTag(GpsDirectory.TagLatitude) && gps.ContainsTag(GpsDirectory.TagLatitudeRef))
                {
                    builder.Append($"{gps.GetDescription(GpsDirectory.TagLatitudeRef)} {gps.GetDescription(GpsDirectory.TagLatitude)} ");
                }
                else
                {
                    result.Errors.Add("Tag 'GPS Latitude' is not found in the 'IFD0' directory.");
                }

                if (gps.ContainsTag(GpsDirectory.TagLongitude) && gps.ContainsTag(GpsDirectory.TagLongitude))
                {
                    builder.Append($"{gps.GetDescription(GpsDirectory.TagLongitudeRef)} {gps.GetDescription(GpsDirectory.TagLongitude)} ");
                }
                else
                {
                    result.Errors.Add("Tag 'GPS Longitude' is not found in the 'IFD0' directory.");
                }

                if (!Coordinate.TryParse(builder.ToString(), out var coordinate))
                {
                    result.Errors.Add("GPS data can not be parsed from the file metadata.");
                }
                else
                {
                    result.Latitude = coordinate.Latitude.ToDouble();
                    result.Longitude = coordinate.Longitude.ToDouble();
                }
            }

            return Task.FromResult(result);
        }
    }
}
