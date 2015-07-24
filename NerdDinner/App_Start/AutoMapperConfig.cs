using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using NerdDinner.Models;

namespace NerdDinner
{
    public class AutoMapperConfig
    {
        public static void Register(HttpConfiguration config)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MobileDinner, Dinner>()
                    .ForMember(dinner => dinner.DinnerID, map => map.MapFrom(mobileDinner => Int16.Parse(mobileDinner.Id)));

                cfg.CreateMap<Dinner, MobileDinner>()
                    .ForMember(mobileDinner => mobileDinner.Id, map => map.MapFrom(dinner => dinner.DinnerID.ToString()))
                    .ForMember(mobileDinner => mobileDinner.CreatedAt, map => map.UseValue<DateTimeOffset?>(null))
                    .ForMember(mobileDinner => mobileDinner.UpdatedAt, map=> map.UseValue<DateTimeOffset?>(null))
                    .ForMember(mobileDinner => mobileDinner.Deleted, map => map.UseValue<bool>(false));
            });
        }
    }
}