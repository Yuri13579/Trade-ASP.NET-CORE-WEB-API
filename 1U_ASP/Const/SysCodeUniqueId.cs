using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Const
{
    public static class SysCodeUniqueId
    {
        public class Country
        {
            public const int UniqueIdMin = 1005;
            public const int UniqueIdMax = 1985;
        }

        public class ForeignLanguage
        {
            public const int UniqueIdMin = 129010;
            public const int UniqueIdMax = 130300;

            public const int ForeignLanguageEnglish = 129330;
        }

        public class ConverseType
        {
            public const int Chat = 42000;
            public const int Group = 42010;
        }

        public class JobSearchPlatforms
        {
            public const int DOU = 40010;
            public const int WorkUa = 40020;
            public const int RabotaUa = 40030;
            public const int hhUa = 40040;
        }

        public class MessageType
        {
            public const int OfferVacancy = 43000;
            public const int Personal = 43010;
            public const int TopCandidateOffer = 43020;
        }

        public class FileType
        {
            public const int PersonPhoto = 20050;
            public const int Resume = 20060;
        }

        public class NoteSettingsName
        {
            public const int AddressedPersonalNote = 45840;
        }

        public class NotificationType
        {
            public const int InvitationBase = 121010;
            public const int InvitationForNew = 121020;
            public const int CustomMessage = 121030;
        }


        public const int QueryPropertyRole = 18010;
        public const int AWWCorApplicantStatusUnderReview = 23110;
        public const int AWWCorApplicantStatusTechInterview1 = 23120;
        public const int AWWCorApplicantStatusCustomersRefusal = 23220;
        public const int ContactTypeEmailAddress = 100010;
        public const int ContactTypeMobilePhone = 100020;
        public const int ContactTypePhone = 100030;
        public const int ContactTypeSkype = 100050;
        public const int ContactTypeLinkedin = 100060;
        public const int ContactTypeWebsite = 100070;
        public const int CompanyContactEmail = 100200;
        public const int CurrentLocationAddress = 31010;
        public const int RegisterAddress = 31020;
        public const int EmployeeCurrentLocationAddress = 31040;
        public const int EmployeeRegisterAddress = 31050;


        public const int HeadquartersAddress = 31501;
        public const int ApplicantInterview = 32010;
        public const int ResponsibleManagerInterview = 32020;
        public const int ContactLink = 108030;
        public const int CustomerResumeLink = 108040;
        public const int ProfileTypeCompanyOwner = 101001;
        public const int ProfileTypeCompanyExecutive = 101002;
        public const int ProfileTypeEmployerMin = 101000;
        public const int ProfileTypeCustomerRangeMin = 101005;
        public const int ProfileTypeCustomerOwner = 101006;
        public const int ProfileTypeCustomerAssistant = 101009;
        public const int ProfileTypeEmployer = 101010;
        public const int ProfileTypeEmployerEmployed = 101020;
        public const int ProfileTypeEmployee = 101050;
        public const int ProfileTypeEmployeeEmployed = 101060;
        public const int ProfileTypeAgent = 101070;
        public const int ProfileTypeEmployeeApplicant = 101100;
        public const int ProfileSkillSkill = 103010;
        public const int ProfileHourlyRateHourlyRate = 106030;
        public const int ProfileEmploymentTypeEmploymentType = 106080;
        public const int ProfileEmployeeLocationEmployeeLocation = 106090;
        public const int CompanyDepartmentDepartment = 107020;
        public const int CompanyPositionPosition = 107030;
        public const int CompanyPropertyCompanyNameHistory = 107060;
        public const int CompanyPropertyIsCompanyRecruiter = 107070;
        public const int LinkTypeJobSpecAttach = 108050;
        public const int RateTypeHourlyRate = 109010;
        public const int RateTypeMonthlyRate = 109050;
        public const int OurRateTypeStaticRate = 110010;
        public const int OurRateTypePercentageRate = 110020;
        public const int ProfileActionTypeEmployeeApplicant = 117040;
        public const int ProfileActionTypeEmployeeEmployed = 117050;
        public const int ApplicantSysCodeStatusApplicantSysCodeStatus = 120020;
        public const int JobSpecificationPropertyJobSpecificationStatus = 124090;
        public const int JobSpecificationRequiredDocument = 124100;
        public const int JobSpecificationProfileTypeManager = 125010;
        public const int JobSpecificationProfileTypeCandidate = 125020;
        public const int JobSpecificationResponsibleManager = 125030;
        public const int ApplicationResponsibleManager = 125040;
        public const int NoteTypeFlexibleEvent = 128020;
        public const int NoteTypeTextMessage = 128030;
        public const int FileTypeReceiptAttachment = 33010;
        public const int CorrespondenceAddress = 31502;
        public const int YouSendTooManyEmails = 139000;
        public const int TheEmailsLooksLikeSpam = 139010;
        public const int YouSendIrrelevantContent = 139020;
        public const int ReadersDidNotKnowTheyWereSubscribing = 139030;
        public const int EmailsNotTailoredToReaderPreferences = 139040;
        public const int TooMuchOrTooLittleContent = 139050;
        public const int Other = 139060;
        public const int PersonImage = 141000;
        public const int NewCompany = 142000;
        public const int CompanyApproved = 142010;
        public const int CompanyNotApproved = 142020;
        public const int CompanyDeleted = 142030;
        public const int CompanySentRepeatConfirmation = 142040;

        public const int NewJobBoard = 143000;
        public const int JobBoardApproved = 143010;
        public const int JobBoardNotApproved = 143020;
        public const int JobBoardDeleted = 143030;

        #region LegalDocStatus
        public const int LegalDocStatusApproved = 34501;
        public const int LegalDocStatusRefused = 34502;
        public const int LegalDocStatusNotConfirmed = 34503;
        public const int LegalDocStatusNoNeedConfirmation = 34504;
        #endregion

        #region DBFrom
        public const int DBFromNewSystem = 36010;
        public const int DBFromOldSystem = 36020;
        public const int DBFromLinkedin = 36030;
        public const int DBFromFromManagers = 36040;
        #endregion

        public const int ProfileTypeCompanyAssistant = 101003;

        public const int Azure = 126010;
        public const int Email = 126020;
        public const int AzureEmail = 126030;

        public const int UnsubscribeDescription = 139000;


        public class ApplicantStatus
        {
            public static List<int> RefusalStatuses = new List<int> { Closed, ApplicantsRefusal, CustomersRefusal, RecruitersRefusal };
            public static List<int> CandidateStatuses = new List<int> { Closed, ApplicantsRefusal, CustomersRefusal, RecruitersRefusal, Onboarding, Associate, HoldOn };

            public const int New = 23010;
            public const int UnderReview = 23110;
            public const int TechInterview1 = 23120;
            //    public const int Techinterview2 = 23130;
            //public const int Techinterview3 = 23140;
            //public const int Techinterview4 = 23150;
            //public const int OfferConfirmation = 23160;
            //public const int DocumentsSent = 23170;
            //public const int DocumentAccepted = 23180;
            //public const int AcceptedByCustomer = 23190;
            public const int Associate = 23200;
            public const int RecruitersRefusal = 23210;
            public const int CustomersRefusal = 23220;
            public const int ApplicantsRefusal = 23230;
            public const int Closed = 23240;
            public const int Onboarding = 23250;
            public const int Disabled = 23260;
            public const int Refusal = 23270;
            public const int AllStatuses = 23280;
            public const int ActiveStatuses = 23290;
            public const int HoldOn = 23300;
        }


        public const int PersonalContact = 41000;
        public const int WorkContact = 41010;
        public const int PersonalEmail = 41020;
        public const int PersonalSkype = 41030;
        public const int PersonalPhone = 41040;
        public const int PersonalLinkedIn = 41050;
        public const int WorkEmail = 41060;
        public const int WorkSkype = 41070;
        public const int WorkPhone = 41080;
        public const int WorkLinkedIn = 41090;
        public const int EmployeePersonalContact = 41100;
        public const int EmployeeWorkContact = 41110;
        public const int EmployeePersonalEmail = 41120;
        public const int EmployeePersonalSkype = 41130;
        public const int EmployeePersonalPhone = 41140;
        public const int EmployeePersonalLinkedIn = 41150;
        public const int EmployeeWorkEmail = 41160;
        public const int EmployeeWorkSkype = 41170;
        public const int EmployeeWorkPhone = 41180;
        public const int EmployeeWorkLinkedIn = 41190;
        public const int AllCandidates = 147000;
        public const int CandidatesWhoAppliedEarlier = 147010;
        public const int CandidatesWhoWereEmployed = 147020;
        public const int UnsubscribeAllChats = 140000;
        public const int UnsubscribeSingleChat = 140010;
        public const int UnsubscribeAllVacancy = 140020;
        public const int UnsubscribeSingleVacancy = 140030;
        public const int UnsubscribeFromOfferVacancyAll = 148000;
        public const int UnsubscribeVacancyBySkill = 148010;
        public const int UnsubscribeVacancyBySkillAndAll = 148020;
        public const int SubscribeSingleVacancy = 148030;
        public const int SubscribeAllVacancy = 148040;
        public const int SubscribeAllVacancyAndSkills = 148050;
        public const int AccountNumber = 107080;
        public const int RoutingNumber = 107090;
        public const int AccountNumber2 = 107100;
        public const int RoutingNumber2 = 107110;
        public const int BankName = 107120;
        public const int BankName2 = 107130;
    }
}
