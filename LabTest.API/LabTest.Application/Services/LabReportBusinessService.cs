using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LabTest.Application.DTO;
using LabTest.Application.IBusinessService;
using LabTest.Domain.Interfaces;
using LabTest.Domain.Models;
using LabTest.Shared;

namespace LabTest.Application.Services
{
    public class LabReportBusinessService : Profile, ILabReportBusinessService
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
        public LabReportBusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add/Update LabReport
        /// </summary>
        /// <param name="labReportModel">Labreport model to add/update</param>
        /// <returns>status</returns>
        public async Task<string> SaveLabReport(AddLabReportDTO labReportDto, string action)
        {
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddLabReportDTO, LabReport>()
                        .ForMember(x => x.IsDeleted, o => o.Ignore())
                        .ForMember(x => x.LabTestMaster, o => o.Ignore())
                        .ForMember(x => x.UpdatedOn, o => o.Ignore())
                        .ForMember(x => x.Patient, o => o.Ignore());
            });
            mapper = conf.CreateMapper();
            var labReport = mapper.Map<LabReport>(labReportDto);

            var status = await this.unitOfWork.LabReport.SaveLabReport(labReport, action);
            if (status == StaticMessage.Success)
                await this.unitOfWork.SaveAsync();
            return status;
        }

        /// <summary>
        /// Delete lab report based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        public async Task<string> DeleteLabReport(int Id)
        {
            var status = await this.unitOfWork.LabReport.DeleteLabReport(Id);
            if (status == StaticMessage.Success)
                await this.unitOfWork.SaveAsync();
            return status;
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labReportId</param>
        /// <returns>Colletion of LabReports</returns>
        public async Task<List<GetLabReportDTO>> GetLabReports(int Id = 0)
        {
            List<GetLabReportDTO> labReportDTOs = new List<GetLabReportDTO>();
            var labReports = await this.unitOfWork.LabReport.GetLabReports(Id);
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LabReport, GetLabReportDTO>();
            });
            mapper = conf.CreateMapper();
            labReportDTOs = mapper.Map<List<GetLabReportDTO>>(labReports);

            return labReportDTOs;
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="labReportId"> Filter to fetch report baed on labReportId</param>
        /// <param name="labTestId">Filter to fetch report baed on labTestId</param>
        /// <param name="patientId">Filter to fetch report baed on patientId</param>
        /// <param name="startDate">Filter to fetch report baed on startDate</param>
        /// <param name="endDate">Filter to fetch report baed on endDate</param>
        /// <returns>Colletion of LabReports</returns>
        public async Task<List<GetDetailLabReport>> GetDetailLabReports(int? labReportId, int? labTestId, int? patientId, DateTime? startDate, DateTime? endDate)
        {
            var labReports = await this.unitOfWork.LabReport.GetDetailLabReports(labReportId, labTestId, patientId, startDate, endDate);
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LabReport, GetDetailLabReport>()
                        .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Patient.PatientName))
                        .ForMember(d => d.EmailId, o => o.MapFrom(s => s.Patient.EmailId))
                        .ForMember(d => d.MaxLimit, o => o.MapFrom(s => s.LabTestMaster.MaxLimit))
                        .ForMember(d => d.MinLimit, o => o.MapFrom(s => s.LabTestMaster.MinLimit))
                        .ForMember(d => d.SampleType, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.SampleType), s.LabTestMaster.SampleTypeId) == true ?
                                     ((Enums.SampleType)Convert.ToInt32(s.LabTestMaster.SampleTypeId)).ToString() : "N/A"))
                         .ForMember(d => d.TestType, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.TestType), s.LabTestMaster.TestTypeId) == true ?
                                     ((Enums.TestType)Convert.ToInt32(s.LabTestMaster.TestTypeId)).ToString() : "N/A"));
            });
            mapper = conf.CreateMapper();
            return mapper.Map<List<GetDetailLabReport>>(labReports);
        }
        #endregion
    }
}
