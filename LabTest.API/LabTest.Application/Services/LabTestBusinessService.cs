using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LabTest.Application.DTO;
using LabTest.Application.IBusinessService;
using LabTest.Shared;
using LabTest.Domain.Interfaces;
using LabTest.Domain.Models;

namespace LabTest.Application.Services
{
    public class LabTestBusinessService :Profile, ILabTestBusinessService
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable of IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Automapper interface
        /// </summary>
        private IMapper mapper;
        #endregion

        #region Contructor

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public LabTestBusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add/Update LabTest
        /// </summary>
        /// <param name="labTest">Model to add/update</param>
        /// <returns>status</returns>
        public async Task<string> SaveLabTest(AddLabTestDTO labTestDto, string action)
        {
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddLabTestDTO, LabTestMaster>()
                        .ForMember(x => x.IsDeleted, o => o.Ignore())
                        .ForMember(x => x.UpdatedOn, o => o.Ignore())
                        .ForMember(x => x.CreatedOn, o => o.Ignore());
            });
            mapper = conf.CreateMapper();
            var labTest = mapper.Map<LabTestMaster>(labTestDto);
            var status = await this.unitOfWork.LabTest.SaveLabTest(labTest,action);
            if (status == StaticMessage.Success)
                await this.unitOfWork.SaveAsync();
            return status;
        }

        /// <summary>
        /// Delete lab test based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        public async Task<string> DeleteLabTest(int Id)
        {
            var status = await this.unitOfWork.LabTest.DeleteLabTest(Id);
            if (status == StaticMessage.Success)
                await this.unitOfWork.SaveAsync();
            return status;
        }

        /// <summary>
        /// Get all the lab tests based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labTestId</param>
        /// <returns>Colletion of LabTestMaster</returns>
        public async Task<List<GetLabTestDTO>> GetLabTests(int Id = 0)
        {
            List<GetLabTestDTO> labTests = new List<GetLabTestDTO>();
            var listLabTests = await this.unitOfWork.LabTest.GetLabTests(Id);
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LabTestMaster, GetLabTestDTO>()
                        .ForMember(d => d.SampleType, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.SampleType), s.SampleTypeId) == true ?
                                     ((Enums.SampleType)Convert.ToInt32(s.SampleTypeId)).ToString() : "N/A"))
                .ForMember(d => d.TestType, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.TestType), s.TestTypeId) == true ?
                                     ((Enums.TestType)Convert.ToInt32(s.TestTypeId)).ToString() : "N/A"));
            });
            mapper = conf.CreateMapper();
            labTests = mapper.Map<List<GetLabTestDTO>>(listLabTests);
            return labTests;
        } 
        #endregion

    }
}
