******************************************************************************
*	Created By : Umesh Agrawal                                              *
*	Contact    : umesh.agrawal@atos.net                                     *
******************************************************************************
# HCA Lab Test
	This is a Web API application for lab test data handing and reporting developed using .Net Core 5.0

# Problem statement
	Need application that is capable of
	1. Generate authentication token for security access
	2. Creating/Managing Patient securely
	3. Creating/Managing Lab Test securely
	4. Creating/Managing/Reporting Lab Reports securely

# Tables (As model classes for In-Memory DB implementation)
	User
		string Username //Logged in user name
		string Password //User password
		
	Patient
        	int Id //PrimarKey
		string PatientName //Patient Name
		DateTime DateOfBirth //Date of birth of patient
         	Enum PatientGender //Gender of patient (0 - None, 1 - Male, 2 - Female, 3 - Other)
		string EmailId //Patient email id
		string ContactNumber //Patient contact number
		string Address //Patient address
		DateTime CreatedOn // Date of patient entry create
		DateTime UpdatedOn // Date of patient entry modify
		bool isDeleted //To mark for soft delete
		ICollection<LabReport> //Collection of lab reports
	
	LabTestMaster
		int Id //PrimarKey
		Enum TestTypeId //Type of test (1 - Chemical, 2 - Physical)
		Enum SampleTypeId //Type of sample for the test (0 - None, 1 - Blood, 2 - Urine, 3 - Stool, 4 - Swab, 5 - Other)
		Single MinimumRequiredQty //Minimum amount of quantity of sample needed for test
		double MinLimit //Minimum limit of value for the result
		double MaxLimit //Maximum limit of value for the result
		DateTime CreatedOn // Date of patient entry create
		DateTime UpdatedOn // Date of patient entry modify
		bool isDeleted //To mark for soft delete
		string Description //Description of the test
		ICollection<LabReport> LabReports //Collection of lab reports
		
	LabReport
		int Id //PrimarKey
		int PatientId //ForeignKey Patient.Id
		LabTestId  //ForeignKey LabTestMaster.Id
		DateTime SampleReceivedOn //Sample received on
		DateTime SampleTestedOn //Sample tested on
		DateTime ReportCreatedOn //Report cretated on
		double TestResult //Value of test result
		string RefferredBy //Refferred by physician / hospital
		DateTime UpdatedOn // Date of patient entry modify
		bool isDeleted //To mark for soft delete
		string Description //Description of the test
		LabTestMaster LabTestMaster// To store LabTestMaster details
		Patient Patient// To store Patient details
				
# Approach
		Implemention using In-Memory DB
		User credentials in User class
		Patient details in Patient class
		Test details in LabTestMaster class
		Report details in LabReport class
		Delete is soft delete (Marked isDeleted true)
		
#Operations Supported with endpoints
	Operations supported with endpoint details, sample URL and payload information 
	
	1. Endpoint Login
		* Login : (Post : https://localhost:44348/api/User/authenticate)
			{
				"username": "demouser",
				"password": "demopassword"
			}			
	2. Endpoint Patient
		
		* Create : (Post : https://localhost:44348/api/Patient/Create)
			{
				"Id": 0,
				"PatientName": "Test Patient 1",
				"DateOfBirth": "1980-05-25T00:00:00",
				"Gender": 1,
				"EmailId": "testpatient1@gmail.com",
				"ContactNo": "(+91) 98235xxxxx",
				"Address": "Pune, Maharashtra, India - 411018"
			}
		* Update : (Put : https://localhost:44348/api/Patient/Update)
			{
				"Id": 1,
				"PatientName": "Test Patient 1 Modified",
				"DateOfBirth": "1980-05-25T00:00:00",
				"Gender": 1,
				"EmailId": "testpatient1modified@gmail.com",
				"ContactNo": "(+91) 98235xxxxx",
				"Address": "Pune, Maharashtra, India - 411018"
			}
		* Delete : (Delete : https://localhost:44348/api/Patient/Delete?Id=1)
		* GetPatientById : (Get : https://localhost:44348/api/Patient/GetPatientById?Id=1)
		* GetAllPatients : (Get : https://localhost:44348/api/Patient/GetAllPatients)
		* GetAllPatientWithReport: (Get : https://localhost:44348/api/Patient/GetAllPatientWithReport?patientId=1&startDate=2022-01-03&endDate=2022-01-03)

		
	3. Endpoint LabTest
		* Create : (Post : https://localhost:44348/api/LabTest/Create)
			{
				
  				"Id": 0,
  				"TestTypeId": 1,
  				"SampleTypeId": 1,
  				"MinLimit": 1000,
  				"MaxLimit":2000,
  				"Descriptions": "Blood Test"

			}		
		* Update : (Put : https://localhost:44348/api/LabTest/Update)
			{
				
  				"Id": 1,
  				"TestTypeId": 2,
  				"SampleTypeId": 1,
  				"MinLimit": 2000,
  				"MaxLimit":3000,
  				"Descriptions": "Blood Test Modified"

			}		
		* Delete : (Delete : https://localhost:44348/api/LabTest/Delete?Id=1)
		* GetLabTestById: (Get : https://localhost:44348/api/LabTest/GetLabTestById?Id=1)
		* GetAllLabTest : (Get : https://localhost:44348/api/LabTest/GetAllLabTest)
		
	4. Endpoint LabReport
		* Create : (Post : https://localhost:44348/api/LabReport/Create)
			{
  				"Id": 0,
  				"LabTestId": 1,
  				"PatientId": 1,
  				"SampleReceivedOn": "2022-01-03T07:30:09.316Z",
  				"SampleTestedOn": "2022-01-03T07:30:09.316Z",
  				"ReportCreatedOn": "2022-01-03T07:30:09.316Z",
  				"TestResult": 120,
  				"ReferredBy": "Dr. Ramani",
  				"Descriptions": "Normal"
			}		
		* Update : (Put : https://localhost:44348/api/LabReport/Update)
			{
				"Id": 1,
  				"LabTestId": 1,
  				"PatientId": 1,
  				"SampleReceivedOn": "2022-01-03T07:30:09.316Z",
  				"SampleTestedOn": "2022-01-03T07:30:09.316Z",
  				"ReportCreatedOn": "2022-01-03T07:30:09.316Z",
  				"TestResult": 125,
  				"ReferredBy": "Dr. Ramani  Modified",
  				"Descriptions": "Normal"
			}
		* Delete : (Delete : https://localhost:44348/api/LabReport/Delete?Id=1)
		* GetAll : (Get : https://localhost:44367/LabReport/Get)
		* GetById : (Get : https://localhost:44367/LabReport/Get/1)
		* GetLabReports : (Get : https://localhost:44348/api/LabReport/GetLabReports?labReportId=1&labTestId=1&patientId=1&startDate=2000-01-01&endDate=2022-01-01)
		
#Installation
	1. Copy code in a folder
	2. Open LabTest.API soultion using Microsoft Visual Studio (LabTest.API.sln)
	3. Build and Run entire solution with Menu->Build->Build Solution
	4. Application should run in browser using Swagger UI
	5. Postman can also be configured (as per above url and payload details) for generating and passing token
	6. Log file will be created in the Logs folder of the root directory of the API project based on the date

	
#Steps to run with Swagger
	1. Execute endpoint Login (Credentials as in Login endpoint details above) to generate token
	2. Once token is generated, copy the generated token
	3. Click Authorize button in page header to open "Available Authorizations" dialogue
	4. Enter copied token in the text input under value
	5. click Authorize and then Close button
	6. Now you are ready to run, follow sequence as below to handle data dependencies

#Steps to run with Postman
	1. Configure Postman requests as per information above
	2. Execute Login (Credentials as above) to generate token
	3. Once token is generated, copy the generated token to pass with subsequet requests
	4. Follow sequence as below to handle data dependencies
