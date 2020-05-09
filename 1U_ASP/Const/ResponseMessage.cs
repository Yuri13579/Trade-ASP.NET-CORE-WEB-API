﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1U_ASP.Const
{
    public class BadResponses
    {
        public const string SQLDateLimit = "Date cannot be less than 1800 or more than 9000";
        public const string CompanyNameAlreadyExists = "Company name already exists";
        public const string EmailHasBeenAlreadyConfirmed = "Email has been already confirmed";
        public const string AccessCodeIsNotValid = "Access code isn't valid";
        public const string AccessIsDenied = "Access is denied";
        public const string ManagerIsnTFound = "Manager isn't found";
        public const string ManagerProcessingFailed = "Manager processing failed";
        public const string EmployeeProcessingFailed = "Employee processing failed";
        public const string EmploymentIsnTFound = "Employment isn't found";
        public const string EmploymentIsAlreadyExists = "Employment is already exists";
        public const string ModifyingIsNotPermitted = "Modifying isn't permitted";
        public const string ModifyingIsNotAllowed = "Modifying isn't allowed";
        public const string ManagerAlreadyExistsYouCanActivateHim = "Manager already exists, you can activate him";
        public const string ManagerDataIsBroken = "Manager data is broken";
        public const string PersonIsnTFound = "Person isn't found";
        public const string GroupSettingsIsnTFound = "GroupSettings isn't found";
        public const string NoteSettingsIsnTFound = "NoteSettings isn't found";
        public const string ProcessFailed = "Process failed";
        public const string IsnTPermittedToChangeAnotherSData = "Isn't permitted to change another's data";
        public const string PropertyProcessingException = "Property processing exception";
        public const string ModifyingIsnTAllowed = "Modifying isn't allowed";
        public const string ModifyingIsnTPermitted = "Modifying isn't permitted";
        public const string CustomerIsnTFound = "Customer isn't found";
        public const string StartDateBiggerThenEndDate = "Start date bigger then end date";
        public const string ApplicationToThisJobAlreadyExists = "Application to this job already exists";
        public const string ModifyingFailed = "Modifying failed";
        public const string ExternalLoginProviderIsNotFound = "External login provider isn't found";
        public const string EmployerNameIsnTAllowed = "Employer Name isn't allowed";
        public const string CreatedSuccessfully = "Created successfully";
        public const string CreationFailed = "Creation failed";
        public const string ChangingIsnTPermitted = "Changing isn't permitted";
        public const string EmployerIsnTFound = "Employer isn't found";
        public const string CandidateIsnTFound = "Candidate isn't found";
        public const string TimeReportIsNotFound = "TimeReport isn't found";
        public const string NoDetailsFound = "No details found";
        public const string TimeReportIsManagedAndNotAvailableMore = "TimeReport is managed and not available more";
        public const string TimeWorkedShouldBeLessOrEqualBillingHours = "TimeWorked should be less or equal billing hours, input additional hours in the OverTimeWorked";
        public const string VacationIsNotFound = "Vacation isn't found";
        public const string ChangingIsnTAllowed = "Changing isn't allowed";
        public const string NewUserOrProviderIsNotAttached = "New user or provider isn't attached";
        public const string TokenIsNotValid = "Token isn't valid";
        public const string ProviderIsNotSupported = "Provider isn't supported";
        public const string FaceBookLoginIsAttachedToTheDifferentAccount = "FaceBook login is attached to the different account";
        public const string FaceBookLoginIsAlreadyAttached = "FaceBook login is already attached";
        public const string LinkedLoginIsAttachedToTheDifferentAccount = "Linkedin login is attached to the different account";
        public const string LinkedLoginIsAlreadyAttached = "Linkedin login is already attached";
        public const string GoogleLoginIsAttachedToTheDifferentAccount = "Google login is attached to the different account";
        public const string GoogleLoginIsAlreadyAttached = "Google login is already attached";
        public const string YouShouldUseAnotherEmailAccountForApplyingOrCreateNew = "You should use another email (account) for applying (or create new)";
        public const string ThisAccountEmailIsAlreadyUsed = "This account (email) is already used";
        //public const string ThisEmailIsAlreadyUsed = "This email is already used";
        public const string ItIsNecessaryToAssignAnotherManager = "It is necessary to assign another manager";
        public const string ThisBillingPeriodIsClosedAndCanTBeModified = "This BillingPeriod is closed and can't be modified";
        public const string UsedPTOsMoreThenAvailable = "Used PTOs more then available";
        public const string ThisBillingPeriodIsConfirmedAndKeyFieldsCanTBeModified = "This BillingPeriod is confirmed and key fields can't be modified";
        //public const string ThisBillingPeriodIsNotConfirmedAndCanNotBeClosed = "This BillingPeriod isn't confirmed and can't be closed";
        public const string QuarterlyRewardShouldBeAppliedOnlyTwiceOrLess = "Quarterly reward should be applied only twice or less";
        public const string QuarterlyRewardMustBeAppliedInQuarterTime = "Quarterly reward must be applied in quarter time";
        public const string AnnualBonusMustBeAppliedInDecembersBilling = "Annual bonus must be applied in December's billing";
        public const string ThisBillingPeriodIsClosedAndCanNotBeModified = "This BillingPeriod is closed and can't be modified";
        public const string MenuLevelIsNotPermitted = "Menu level isn't permitted";
        public const string UserIsnTFound = "User isn't found";
        public const string IPIsntValid = "IP is not valid";
        public const string TokenIsBroken = "Token is broken";
        public const string PasswordsDonTMatch = "Passwords don't match";
        public const string EmailIsnTFound = "Email isn't found";
        public const string UserAlreadyExists = "User already exists";
        public const string ThisEmailIsAlreadyUsedBy = "This email is already used by ";
        public const string ExternalLoginAttachingOperationFailed = "External login attaching operation failed";
        public const string EmailAddressIsnTConfirmed = "Your email address isn't confirmed, check your email";
        public const string PasswordIsnTValid = "Password isn't valid";
        public const string YouCanTryAgainInMinutes = "You can try again in 10 minutes";
        public const string RequiredAtLeastOneLinkLinkedinOrResume = "Requiered at least one link - linkedin or resume";
        public const string BillingPeriodIsNotFound = "BillingPeriod isn't found";
        public const string PhotoIsNotFound = "Photo is not found";
        //public const string DeletedFailed = "Deleted Failed";
        public const string UpdatedFailed = "Updated Failed";
        public const string CompanyNotApproved = "Company Not Approved";
        public const string PtoAwardMustBeFrom0To5 = "PtoAward must be from 0 to 5";
        public const string LinkedinLinkIsntValid = "Linkedin link isn't valid";
        public const string ResumeLinkIsntValid = "Resume link isn't valid";
        public const string InMainContactAllFieldsAreRequired = "In main contact all fields are required";
        public const string IncorrectTypeOfContact = "Incorrect type of contact";
        public const string SuchTemplateAlreadyExist = "Such template already exist";
        public const string EmailTemplateIsNotFound = "Email template is not found";
        public const string EmailTemplatesEquals = "Email templates are equal";
        public const string IncorrectInputData = "Incorrect input data";
        public const string JobSpecificationDoesntExist = "Job specification doesn't exist";
        public const string InvalidFileFormat = "Invalid file format";
        public const string ApplicantIsnTFound = "Applicant isn't found";
        public const string TemplateIsNotFound = "Template is not found";
    }

    public class GoodResponses
    {
        public const string SuccessfullyUnsubscribed = "Unsubscribed successfully";
        public const string AddedSuccessfully = "Added successfully";
        public const string UpdatedSuccessfully = "Updated successfully";
        public const string LoggedSuccessfully = "Logged successfully";
        public const string FilesUploadedSuccessfully = "Files uploaded successfully";
        public const string UndeletedSuccessfully = "Undeleted successfully";
        public const string DisabledSuccessfully = "Disableded Successfully";
        public const string ActivatedSuccessfully = "Activated Successfully";
        public const string MarkedSuccessfully = "Marked successfully";
        public const string DefaultAccountLevelIsChanged = "Default account level is changed";
        public const string ModifiedSuccessfully = "Modified successfully";
        public const string DeletedSuccessfully = "Deleted successfully";
        public const string NotificationsArchivedSuccessfully = "Notifications Archived Successfully";
        public const string ConversationIsAdded = "Conversation is added";
        public const string FacebookLoginAttached = "Facebook login attached";
        public const string LinkedinLoginAttached = "Linkedin login attached";
        public const string GoogleLoginAttached = "Google login attached";
        public const string InvitationEmailsAreSent = "Invitation emails are sent";
        public const string EmailsAreSent = "Emails are sent";
        public const string CandidateApplicationIsSent = "Candidate application is sent";
        public const string PasswordIsReset = "Password is reset";
        public const string ToConfirmYourNewEmailPlzCheckEmail = "To confirm your new email plz check email";
        public const string ToResetYourPasswordCheckEmail = "To reset your password plz check email";
        public const string EmailIsReset = "Email is reset";
        public const string SentSuccessfully = "Sent successfully";
        public const string AddedSuccessfullyButSomeAppliesToThisJobAlreadyExists = "Added successfully but some applies to this job already exists :";
        public const string EntityExist = "Entity exist";
    }

}
