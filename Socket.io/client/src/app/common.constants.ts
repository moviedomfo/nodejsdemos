
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';


const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
headers.append('Access-Control-Allow-Headers', 'Content-Type');
headers.append('Access-Control-Allow-Methods', '*');
headers.append('Access-Control-Allow-Origin', '*');

let header_httpClient_contentTypeJson = new HttpHeaders({ 'Content-Type': 'application/json' });
header_httpClient_contentTypeJson.append('Access-Control-Allow-Methods', '*');
header_httpClient_contentTypeJson.append('Access-Control-Allow-Headers', 'Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With');
header_httpClient_contentTypeJson.append('Access-Control-Allow-Origin', '*');

let header_httpClient_form_urlencoded = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
header_httpClient_form_urlencoded.append('Access-Control-Allow-Methods', '*');
header_httpClient_form_urlencoded.append('Access-Control-Allow-Headers', 'Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With');
header_httpClient_form_urlencoded.append('Access-Control-Allow-Origin', '*');



export  const AppConstants = {
    AppProducion: environment.production,
    AppVersion: environment.version,
    AppVersionBuild: environment.version,
    baseURL: environment.baseURL + 'api/',
    InstitutionId:   'a92a227f-f39a-4ad5-b726-4a84963e5dd3',
    emptyGuid : '00000000-0000-0000-0000-000000000000'

};


export  enum  ParamTypeEnum
      {
        EntityStatus = 500,
        OrderStatus= 520,
        PedidoStatus= 540,
        StockType= 600,
        TipoDocumento = 700,
        Especialidad = 550,
        Profesion = 100,
        EstadoCivil=  750,
        TipoRecepcion =  200,
        TipoMedioContacto =  1000,
        Paises =  1050,
        Localidad =  1200,
        Provincia =  1100,
        SubCategoriaLibro =  5000,
        CategoriaLibro =  1000,
        Parentesco= 2000
      }



export const CommonValuesEnum =
    {
        TodosComboBoxValue : -1000,
        VariosComboBoxValue : -2000,
        SeleccioneUnaOpcion : -3000,
        Ninguno : -4000,
        /// <summary>
        /// Esta opcion es usada para seleccion de Mutuales .- Caso Sin mutual particular
        /// </summary>
        Particular : -5000
    };

export const DayNamesIndex_Value_ES = [
        {'name' : 'Sabado' , 'index'  : 0, 'bidValue': 1},
        {'name' : 'Viernes' , 'index'  : 1, 'bidValue': 2 },
        {'name' : 'Jueves' , 'index'  : 2, 'bidValue': 4 },
        {'name' : 'Miercoles' , 'index'  : 3, 'bidValue': 8 },
        {'name' : 'Martes' , 'index'  : 4, 'bidValue': 16 },
        {'name' : 'Lunes' , 'index'  : 5, 'bidValue': 32 },
        {'name' : 'Domingo' , 'index'  : 6, 'bidValue': 64 },
     ];

export const AppoimantsStatus_SP = {
    Reservado : 630,
    EnAtencion : 631,
    Cerrado : 632,
    Cancelado : 633,
    Expirado : 634,
    EnEspera : 635,
    Libre : 636,
    Nulo : 637
};
export const AppoimantsStatus_SP_type =
{

    Entreturno : 638,
    Sobreturno : 639
};

  /// <summary>
    /// Estados de una subscripcion enviada para pertenecer a una institución
    /// </summary>
export const SubscriptionRequestStatus =
    {
        EnEspera : 650,
        Rechazado : 651,
        Expirado : 652,
        Null : 653

    };
export const DayNamesIndex_ES =
    {
        // SAB	VIE	JUE	MIE	MAR	LUN	DOM
        Sabado : 0,
        Viernes : 1,
        Jueves : 2,
        Miercoles : 3,
        Martes : 4,
        Lunes : 5,
        Domingo : 6
    };
export const  WeekDays_EN =
    {

        Sunday : 1,

        Monday : 2,

        Tuesday : 4,

        Wednesday : 8,

        Thursday : 16,

        Friday : 32,
        //
        // Summary:
        //     Specifies work days (Monday, Tuesday, Wednesday, Thursday and Friday).
        WorkDays : 62,
        //
        // Summary:
        //     Specifies Saturday.
        Saturday : 64,
        //
        // Summary:
        //     Specifies Saturday and Sunday.
        WeekendDays : 65,
        //
        // Summary:
        //     Specifies every day of the week.
        EveryDay : 127
    };

     //
    // Summary:
    //     Specifies the day of the week c# System
export const DayOfWeek =
    {
        Sunday : 0,
        Monday : 1,
        Tuesday : 2,
        Wednesday : 3,
        Thursday : 4,
        Friday : 5,
        Saturday : 6
    };

export const MonthsShortName_ES =
    {
        ENE : 1,
        FEB : 2,
        MAR : 3,
        ABR : 4,
        MAY : 5,
        JUN : 6,
        JUL : 7,
        AGO : 8,
        SET : 9,
        OCT : 10,
        NOV : 11,
        DIC : 12

    };

export const MonthsNames_ES =
    {
        Enero : 1,
        Febrero : 2,
        Marzo : 3, Abril : 4, Mayo : 5, Junio : 6, Julio : 7, Agosto : 8,
        Septiembre : 9,
        Octubre : 10,
        Noviembre : 11,
        Diciembre : 12

    };

export const CommonParams = {
    TodosComboBoxValue: {
        ParamId: -1000,
        Name: 'Todos'
    },
    VariosComboBoxValue: {
        ParamId: -2000,
        Name: 'Varios'
},
    SeleccioneUnaOpcion : {
        ParamId: -3000,
        Name: 'Seleccione una opcion'
},
    Ninguno : {
        ParamId: -4000,
        Name: 'Ninguno'
            },

    Particular :  {
        ParamId: -5000,
        Name: 'Ninguno'
            }

};
export enum  PersonStatus
    {
        Activo = 501,
        Inactivo = 502,
        Desvinculado = 503,
        PendienteAuth = 304

    }
export enum  MotivoConsultaEnum
    {
        CreateStock = 0,
        UpdateStock= 1,
        CreateProvider= 2,
        UpdateProvider= 3,
        QueryPerson_NoUpdate= 4,
        QueryStock_NoUpdate = 5,
        /// <summary>
        /// Asocia a la institucion un provider ya existente
        /// </summary>
        AsociateProvider= 6,
        CreateOrder = 7,
        UpdateOrder= 8,
        QueryOrder_NoUpdate = 9,
        QueryProviderPage = 10,
        CreateAccount= 11,
        UpdateAccount= 12,
        QueryAccount= 13
    }

export enum OrderStatusEnum
    {
            OnCreation = 525,
            Created = 521,
            Approrved= 522,
            SendedToProviders= 523,
            Closed= 524,

    }
export enum PedidoStatusEnum
    {
        OnCreation = 540,
        Created = 541,
        Approrved = 542,
        Rechazado = 543,
        SendedToProviders = 544,
        Recived = 545,
        Closed = 546

    }

export enum SolicitudRegistroConexionEnum {
        Nueva = 1,
        EnRevicion = 2,
        Cerrado = 3,
        Cancelado = 4
    }

export enum DialogResultEnum{
    OK = 1,
    Cancel = 2
}


export enum AppRulesEnum{
  sol_reg_conexion_list,
  cotizaciones_manage,
  cotizaciones_query,
  stock_query,
  sec_users,
  sol_reg_conexion_manage,
  stock_manage,
  cotizaciones_sendMailToProviders,
  sec_roles_rules,
}

export enum AlertTypeEnum
{
    NuevaSolicitudConexionAgua = 6000,
    NuevaSolicitudConexionLuz = 6001,
    NuevaSolicitudRegistroSocio = 6002,
    Cotizacion = 6003,
    DeclaracionArtefactos = 6004,

}

export enum AlertStateEnum
{
    Nueva = 0,
    Leida = 1,
    EnCurso = 2,
    Cerrada = 3,

}
export enum MessageStateEnum
{
    unreaded = 0,
    Readed = 1,
    Closed = 2,

}

export enum MessageStateEnum_ES {
  Nuevo = 0,
  Leido = 1,
  Cerrado = 2
}
