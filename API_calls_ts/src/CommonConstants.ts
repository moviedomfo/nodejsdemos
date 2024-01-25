import dotenv from 'dotenv';

dotenv.config();



export const AppConstants = {

  BASE_URL: 'http://localhost:5178',
  SportClubGuid: "d379670c-21b5-4ddd-a4ec-f5d34156b861",
  DevUserGuid: "5FC54C09-9EAB-4025-821B-0B799ABE4F98",
  EmptyGuid: '00000000-0000-0000-0000-000000000000',
  STORAGE_NAME: "sportClubReact",
  STORAGE_NAME_MANUAL: `persist:sportClubReact`,


  COMPANY: 'Pelsoft',
  HEADERS: {
    crossDomain: "true",
    Accept: "application/json",
    //Authorization: `Bearer ${localStorage.token}`,
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, OPTIONS",
    "Access-Control-Allow-Headers": "Origin, Content-Type, Authorization, X-Auth-Token",
    "Content-Type": "application/json",
  },
  Client_Id: 'sportClub',
  Client_Secret: 'pletorico28',
  Sec_Provider: 'sportClub',


};
export const AppConst_Paths = {
  ACTIVITY_FEE_API_URL: `${AppConstants.BASE_URL}/api/ActivityFeeAPI`,
  FEE_API_URL: `${AppConstants.BASE_URL}/api/feeAPI`,
  MEMBER_API_URL: `${AppConstants.BASE_URL}/api/MemberAPI`,
  MEMBER_FROMEXTERNAL_API_URL: `${AppConstants.BASE_URL}/api/ExternalMemberAPI`,
  PERSON_API_URL: `${AppConstants.BASE_URL}/api/PersonAPI`,
  PROFESSIONAL_API_URL: `${AppConstants.BASE_URL}/api/ProfessionalAPI`,

  REPORTS_API_URL: `${AppConstants.BASE_URL}/api/ReportsAPI`,
  FACTURA_API_URL: `${AppConstants.BASE_URL}/api/FacturaAPI`,
  AUTH_API_URL: `${AppConstants.BASE_URL}/api/AccountAPI`,
  ParamsAPI_URL: `${AppConstants.BASE_URL}/api/ParamsAPI`,
  FacturaCompraAPI_URL: `${AppConstants.BASE_URL}/api/FacturaCompraAPI`,
  ProviderAPI_URL: `${AppConstants.BASE_URL}/api/ProviderAPI`,
  MovimientosAPI_URL: `${AppConstants.BASE_URL}/api/movimientosAPI`,
  FacturaAPI_URL: `${AppConstants.BASE_URL}/api/FacturaAPI`,
  Account_API_URL: `${AppConstants.BASE_URL}/api/AccountAPI`,
  Person_API_URL: `${AppConstants.BASE_URL}/api/PersonAPI`,
  RolesAdmin_API_URL: `${AppConstants.BASE_URL}/api/RolesAdminAPI`,
  UserAdmin_API_URL: `${AppConstants.BASE_URL}/api/UserAdminAPI`,
  Professional_API_URL: `${AppConstants.BASE_URL}/api/ProfessionalAPI`,
  REPORT_API_URL: `${AppConstants.BASE_URL}/api/ReportsAPI`,
  CASHFLOW_API_URL: `${AppConstants.BASE_URL}/api/CashFlowAPI`,
  CashflowConcept_API_URL: `${AppConstants.BASE_URL}/api/CashFlowConceptAPI`,
  Activity_API_URL: `${AppConstants.BASE_URL}/api/ActivityAPI`,
  ActivityFee_API_URL: `${AppConstants.BASE_URL}/api/ActivityFeeAPI`,
  TEST_API_URL: `${AppConstants.BASE_URL}/api/test`,
  Member_API_URL: `${AppConstants.BASE_URL}/api/MemberAPI`,
  MemberCategory_API_URL: `${AppConstants.BASE_URL}/api/MemberCategoryAPI`,
  Common_API_URL: `${AppConstants.BASE_URL}/api/commonAPI`,
  Vendor_API_URL: `${AppConstants.BASE_URL}/api/vendorAPI`,
  Event_API_URL: `${AppConstants.BASE_URL}/api/EventAPI`,
  BANCO_API_URL: `${AppConstants.BASE_URL}/api/BancoAPI`,
  ActivityType_API_URL: `${AppConstants.BASE_URL}/api/ActivityTypeAPI`,
  ActivityAssist_API_URL: `${AppConstants.BASE_URL}/api/ActivityAssistAPI`,
  InvoiceCashIncome_API_URL: `${AppConstants.BASE_URL}/api/InvoiceCashIncomeAP`
};


export const drawerWidth = 240;

