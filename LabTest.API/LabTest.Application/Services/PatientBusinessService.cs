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
    public class PatientBusinessService :Profile,IPatientBusinessService
    {
        #region Global Variables
        /// <summary>
        /// Private read only IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Automapper interface
        /// </summary>
        private IMapper mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public PatientBusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Add/Update Patient
        /// </summary>
        /// <param name="patient">Model to add/update</param>
        /// <returns>status</returns>
        public async Task<string> SavePatient(AddPatientDTO patientDto, string action)
        {
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddPatientDTO, Patient>()
                        .ForMember(x => x.IsDeleted, o => o.Ignore())
                        .ForMember(x => x.CreatedOn, o => o.Ignore())
                        .ForMember(x => x.UpdatedOn, o => o.Ignore())
                        .ForMember(x => x.LabReports, o => o.Ignore());
            });
            mapper = conf.CreateMapper();
            var patient = mapper.Map<Patient>(patientDto);
            var status= await this.unitOfWork.Patient.SavePatient(patient,action);
            if (status == StaticMessage.Success)
                await this.unitOfWork.SaveAsync();
            return status;
        }

        /// <summary>
        /// Delete record based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        public async Task<string> DeletePatient(int Id)
        {
            var status = await this.unitOfWork.Patient.DeletePatient(Id);
            if (status == StaticMessage.Success)
                await this.unitOfWork.SaveAsync();
            return status;
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report based on patient Id</param>
        /// <returns>Colletion of Patient</returns>
        public async Task<List<GetPatientDTO>> GetPatients(int Id = 0)
        {
            List<GetPatientDTO> patients = new List<GetPatientDTO>();
            var listPatients = await this.unitOfWork.Patient.GetPatients(Id);
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Patient, GetPatientDTO>()
                        .ForMember(d => d.Gender, o =>o.MapFrom(s=> Enum.IsDefined(typeof(Enums.Gender), s.Gender) == true ?
                                    ((Enums.Gender)Convert.ToInt32(s.Gender)).ToString() : "N/A"));
                      
            });
            mapper = conf.CreateMapper();
            patients = mapper.Map<List<GetPatientDTO>>(listPatients);
            return patients;
        }

        /// <summary>
        /// Get all the records based on the filters or all
        /// </summary>
        /// <param name="patientId">Filter to fetch report based on patientId</param>
        /// <param name="startDate">Filter to fetch report based on startDate</param>
        /// <param name="endDate">Filter to fetch report based on endDate</param>
        /// <returns>Collection of PatientReportModel</returns>
        public async Task<List<GetPatientReportDTO>> GetPatientWithReport(int? patientId, DateTime? startDate, DateTime? endDate)
        {
            List<GetPatientReportDTO> patients = new List<GetPatientReportDTO>();
            var listPatients = await this.unitOfWork.Patient.GetPatientWithReport(patientId, startDate, endDate);
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Patient, GetPatientReportDTO>()
                        .ForMember(d => d.PatientId, o => o.MapFrom(s =>s.Id))
                        .ForMember(d => d.Gender, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.Gender), s.Gender) == true ?
                                     ((Enums.Gender)Convert.ToInt32(s.Gender)).ToString() : "N/A"));
                cfg.CreateMap<LabReport, GetLabReportTestDTO>()
                            .ForMember(d => d.LabReportId, o => o.MapFrom(s => s.Id))
                            .ForMember(d => d.MaxLimit, o => o.MapFrom(s => s.LabTestMaster.MaxLimit))
                            .ForMember(d => d.MinLimit, o => o.MapFrom(s => s.LabTestMaster.MinLimit))
                            .ForMember(d => d.TestDescriptions, o => o.MapFrom(s => s.LabTestMaster.Descriptions))
                            .ForMember(d => d.ReportDescriptions, o => o.MapFrom(s => s.Descriptions))
                            .ForMember(d => d.SampleType, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.SampleType), s.LabTestMaster.SampleTypeId) == true ?
                                                 ((Enums.SampleType)Convert.ToInt32(s.LabTestMaster.SampleTypeId)).ToString() : "N/A"))
                            .ForMember(d => d.TestType, o => o.MapFrom(s => Enum.IsDefined(typeof(Enums.TestType), s.LabTestMaster.TestTypeId) == true ?
                                                 ((Enums.TestType)Convert.ToInt32(s.LabTestMaster.TestTypeId)).ToString() : "N/A")); 
                
            });
            mapper = conf.CreateMapper();
            patients = mapper.Map<List<GetPatientReportDTO>>(listPatients);
            return patients;
        }
        #endregion
    }
}
