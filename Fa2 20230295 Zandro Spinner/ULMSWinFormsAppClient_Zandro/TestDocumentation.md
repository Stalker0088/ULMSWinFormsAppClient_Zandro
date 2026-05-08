# ULMS Testing Documentation

## Test Plan

### Testing Objectives
The objective of testing is to verify that the Umoja Learning Management System supports login validation, student registration, course enrolment, marks capture, report generation, and logout according to expected user requirements.

### Scope of Testing
Testing includes:
- Functional testing
- Input validation testing
- Integration testing
- Performance testing
- Usability testing
- Security testing
- Reliability testing

### Testing Environment
Hardware:
- Windows PC or laptop

Software:
- Windows 10 / Windows 11
- Visual Studio
- .NET 8 SDK
- GitHub
- ULMS Windows Forms application

### Testing Tools Used
- Visual Studio
- Manual test cases
- Windows screenshots
- GitHub
- Stopwatch timing inside report generation

## Test Cases

| Test Case ID | Module | Input Data | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|
| TC001 | Login | admin / Admin123 | Dashboard opens | Dashboard opened | Pass |
| TC002 | Login | wrong / wrong | Login blocked with error | Error shown | Pass |
| TC003 | Login | empty username and password | Required field error shown | Error shown | Pass |
| TC004 | Login | admin / empty password | Required password error shown | Error shown | Pass |
| TC005 | Student Registration | Valid student details | Student saved | Student saved | Pass |
| TC006 | Student Registration | Empty details | Validation message shown | Error shown | Pass |
| TC007 | Student Registration | Duplicate student number | Duplicate blocked | Error shown | Pass |
| TC008 | Course Enrolment | Valid student and course | Enrolment saved | Enrolment saved | Pass |
| TC009 | Course Enrolment | Duplicate enrolment | Duplicate blocked | Error shown | Pass |
| TC010 | Marks Capture | Numeric mark 0-100 | Mark saved | Mark saved | Pass |
| TC011 | Marks Capture | Non-numeric mark | Error message shown | Error shown | Pass |
| TC012 | Reports | Generate report | Report loads with average | Report loaded | Pass |

## Defect Report

| Bug ID | Module | Description | Severity | Priority | Status | Corrective Action |
|---|---|---|---|---|---|---|
| BUG001 | Login | Empty login fields were not properly validated | High | High | Fixed | Added required field validation |
| BUG002 | Login | Invalid login could continue without clear message | High | High | Fixed | Added credential check and error message |
| BUG003 | Student Registration | Blank student records could be saved | Medium | High | Fixed | Added field validation |
| BUG004 | Course Enrolment | Duplicate enrolments could be created | Medium | High | Fixed | Added duplicate enrolment check |
| BUG005 | Marks Capture | Non-numeric or out-of-range marks could be accepted | High | High | Fixed | Added decimal parsing and 0-100 range validation |

## Final QA Report Summary

The ULMS application was tested using manual functional and non-functional testing. The corrected version successfully validates login, student registration, course enrolment, marks capture, report generation, and logout. Five defects were identified and corrected. The system is suitable for controlled assessment deployment.
