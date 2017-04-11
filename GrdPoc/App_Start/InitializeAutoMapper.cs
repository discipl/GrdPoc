using AutoMapper;
using GrdPoc.Models.Entities;
using GrdPoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrdPoc
{
    public static class InitializeAutoMapper
    {
        public static void Initialize()
        {
            CreateModelsToViewModels();
        }

        private static void CreateModelsToViewModels()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ServiceIncidentalContract, ServiceContractViewModel>().ReverseMap();
                cfg.CreateMap<IncidentalContract, ServiceContractViewModel>().ReverseMap();
                cfg.CreateMap<ProductIncidentalContract, ProductContractViewModel>().ReverseMap();
                cfg.CreateMap<IncidentalContract, ProductContractViewModel>().ReverseMap();
                cfg.CreateMap<TrainningIncidentalContract, TrainingContractViewModel>().ReverseMap();
                cfg.CreateMap<IncidentalContract, TrainingContractViewModel>().ReverseMap();

                cfg.CreateMap<IncidentalContract, ExecutionProject>().ReverseMap();

            });

        }
    }
}