using Apprendi.Application.Common.Base;
using Apprendi.Application.Common.Interfaces;
using Apprendi.Application.Factories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TimeZoneConverter;
using TimeZoneNames;

namespace Apprendi.Application.Features.ReferenceData.Queries.GetTimeZones
{
    public class GetTimeZonesQueryHandler : RequestHandler<GetTimeZonesQuery, GetTimeZonesQueryResponse>
    {
        private readonly IApprendiDbContext _context;

        public GetTimeZonesQueryHandler(IResponseFactory<GetTimeZonesQueryResponse> responseFactory, 
                                     IApprendiDbContext context) : base(responseFactory)
        {
            _context = context;
        }

        //public override async Task<GetTimeZonesQueryResponse> Handle(GetTimeZonesQuery request, CancellationToken cancellationToken)
        //{
        //    var currentUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        //
        //    var query = _context.TimeZones
        //        .Select(tz => new
        //        {
        //            TimeZone = tz,
        //            TimeZoneData = _context.TimeZoneData
        //                .Where(tzd => tzd.ZoneName == tz.Id && tzd.TimeStart <= currentUnixTimestamp)
        //                .OrderByDescending(tzd => tzd.TimeStart)
        //                .FirstOrDefault()
        //        })
        //        .Where(t => t.TimeZoneData != null)
        //        .Select(t => new TimeZoneDto
        //        {
        //            Id = t.TimeZone.Id,
        //            Abbreviation = t.TimeZoneData.Abbreviation,
        //            Offset = t.TimeZoneData.GmtOffset,
        //        });
        //
        //
        //    var result = await query.ToListAsync(cancellationToken);
        //
        //    return Response.Success(response =>
        //    {
        //        response.TimeZones = result;
        //    });
        //}

        private TimeZoneDto CreateDto(Domain.Entities.TimeZone timeZone)
        {
            string windowsTimeZoneId = TZConvert.IanaToWindows(timeZone.Id);
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo);

            var abbreviations = TZNames.GetAbbreviationsForTimeZone(timeZone.Id, CultureInfo.CurrentCulture.Name);

            string abbreviation;
            
            if (timeZoneInfo.IsDaylightSavingTime(currentTime))
            {
                abbreviation = abbreviations.Daylight ?? abbreviations.Generic ?? abbreviations.Standard ?? "GMT";
            }
            else
            {
                abbreviation = abbreviations.Standard ?? abbreviations.Generic ?? "GMT";
            }

            var offset = timeZoneInfo.GetUtcOffset(DateTime.Now).TotalHours;

            return new TimeZoneDto
            {
                Id = timeZone.Id,
                Abbreviation = abbreviation,
                Offset = offset
            };
        }

        public override async Task<GetTimeZonesQueryResponse> Handle(GetTimeZonesQuery request, CancellationToken cancellationToken)
        {
            var currentUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var timeZones = await _context.TimeZones
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = timeZones.Select(CreateDto).ToList();

            return Response.Success(response =>
            {
                response.TimeZones = result;
            });
        }

        
    }
}
