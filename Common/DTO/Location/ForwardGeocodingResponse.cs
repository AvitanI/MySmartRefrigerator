using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Common.DTO.Location
{
    /// <summary>
    /// Represent response from open cage api
    /// <seealso cref="https://opencagedata.com/api"/>
    /// This class created by quick type generator
    /// <seealso cref="https://app.quicktype.io/#l=cs&r=json2csharp"/>
    /// </summary>
    public class ForwardGeocodingResponse
    {
        [JsonProperty("documentation")]
        public Uri Documentation { get; set; }

        [JsonProperty("licenses")]
        public List<License> Licenses { get; set; }

        [JsonProperty("rate")]
        public Rate Rate { get; set; }

        [JsonProperty("results")]
        public List<ForwardGeocodingResult> Results { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("stay_informed")]
        public StayInformed StayInformed { get; set; }

        [JsonProperty("thanks")]
        public string Thanks { get; set; }

        [JsonProperty("timestamp")]
        public Timestamp Timestamp { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }

    public class License
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public class Rate
    {
        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("remaining")]
        public long Remaining { get; set; }

        [JsonProperty("reset")]
        public long Reset { get; set; }
    }

    public class ForwardGeocodingResult
    {
        [JsonProperty("annotations")]
        public Annotations Annotations { get; set; }

        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("components")]
        public Components Components { get; set; }

        [JsonProperty("confidence")]
        public long Confidence { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
    }

    public class Annotations
    {
        [JsonProperty("DMS")]
        public Dms Dms { get; set; }

        [JsonProperty("MGRS")]
        public string Mgrs { get; set; }

        [JsonProperty("Maidenhead")]
        public string Maidenhead { get; set; }

        [JsonProperty("Mercator")]
        public Mercator Mercator { get; set; }

        [JsonProperty("OSM")]
        public Osm Osm { get; set; }

        [JsonProperty("UN_M49")]
        public UnM49 UnM49 { get; set; }

        [JsonProperty("callingcode")]
        public long Callingcode { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("geohash")]
        public string Geohash { get; set; }

        [JsonProperty("qibla")]
        public double Qibla { get; set; }

        [JsonProperty("roadinfo")]
        public Roadinfo Roadinfo { get; set; }

        [JsonProperty("sun")]
        public Sun Sun { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }

        [JsonProperty("what3words")]
        public What3Words What3Words { get; set; }
    }

    public class Currency
    {
        [JsonProperty("alternate_symbols")]
        public List<string> AlternateSymbols { get; set; }

        [JsonProperty("decimal_mark")]
        public string DecimalMark { get; set; }

        [JsonProperty("html_entity")]
        public string HtmlEntity { get; set; }

        [JsonProperty("iso_code")]
        public string IsoCode { get; set; }

        [JsonProperty("iso_numeric")]
        public long IsoNumeric { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("smallest_denomination")]
        public long SmallestDenomination { get; set; }

        [JsonProperty("subunit")]
        public string Subunit { get; set; }

        [JsonProperty("subunit_to_unit")]
        public long SubunitToUnit { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("symbol_first")]
        public long SymbolFirst { get; set; }

        [JsonProperty("thousands_separator")]
        public string ThousandsSeparator { get; set; }
    }

    public class Dms
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }
    }

    public class Mercator
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public class Osm
    {
        [JsonProperty("edit_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri EditUrl { get; set; }

        [JsonProperty("note_url")]
        public Uri NoteUrl { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public class Roadinfo
    {
        [JsonProperty("drive_on")]
        public string DriveOn { get; set; }

        [JsonProperty("road", NullValueHandling = NullValueHandling.Ignore)]
        public string Road { get; set; }

        [JsonProperty("road_type", NullValueHandling = NullValueHandling.Ignore)]
        public string RoadType { get; set; }

        [JsonProperty("speed_in")]
        public string SpeedIn { get; set; }
    }

    public class Sun
    {
        [JsonProperty("rise")]
        public Rise Rise { get; set; }

        [JsonProperty("set")]
        public Rise Set { get; set; }
    }

    public class Rise
    {
        [JsonProperty("apparent")]
        public long Apparent { get; set; }

        [JsonProperty("astronomical")]
        public long Astronomical { get; set; }

        [JsonProperty("civil")]
        public long Civil { get; set; }

        [JsonProperty("nautical")]
        public long Nautical { get; set; }
    }

    public class Timezone
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("now_in_dst")]
        public long NowInDst { get; set; }

        [JsonProperty("offset_sec")]
        public long OffsetSec { get; set; }

        [JsonProperty("offset_string")]
        public string OffsetString { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }
    }

    public class UnM49
    {
        [JsonProperty("regions")]
        public Regions Regions { get; set; }

        [JsonProperty("statistical_groupings")]
        public List<string> StatisticalGroupings { get; set; }
    }

    public class Regions
    {
        [JsonProperty("ASIA")]
        public long Asia { get; set; }

        [JsonProperty("IL")]
        public long Il { get; set; }

        [JsonProperty("WESTERN_ASIA")]
        public long WesternAsia { get; set; }

        [JsonProperty("WORLD")]
        public string World { get; set; }
    }

    public class What3Words
    {
        [JsonProperty("words")]
        public string Words { get; set; }
    }

    public class Bounds
    {
        [JsonProperty("northeast")]
        public Geometry Northeast { get; set; }

        [JsonProperty("southwest")]
        public Geometry Southwest { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public class Components
    {
        [JsonProperty("ISO_3166-1_alpha-2")]
        public string Iso31661_Alpha2 { get; set; }

        [JsonProperty("ISO_3166-1_alpha-3")]
        public string Iso31661_Alpha3 { get; set; }

        [JsonProperty("_category")]
        public string Category { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("bus_stop", NullValueHandling = NullValueHandling.Ignore)]
        public string BusStop { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("residential", NullValueHandling = NullValueHandling.Ignore)]
        public string Residential { get; set; }

        [JsonProperty("road", NullValueHandling = NullValueHandling.Ignore)]
        public string Road { get; set; }

        [JsonProperty("road_type", NullValueHandling = NullValueHandling.Ignore)]
        public string RoadType { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("suburb", NullValueHandling = NullValueHandling.Ignore)]
        public string Suburb { get; set; }

        [JsonProperty("town", NullValueHandling = NullValueHandling.Ignore)]
        public string Town { get; set; }
    }

    public class Status
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class StayInformed
    {
        [JsonProperty("blog")]
        public Uri Blog { get; set; }

        [JsonProperty("twitter")]
        public Uri Twitter { get; set; }
    }

    public class Timestamp
        {
            [JsonProperty("created_http")]
            public string CreatedHttp { get; set; }

            [JsonProperty("created_unix")]
            public long CreatedUnix { get; set; }
        }
}
