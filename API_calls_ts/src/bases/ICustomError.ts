
export class CustomError extends Error{
     public severity: 'warning' | 'error' = "warning";
    public code?: string;
    public status? : number;
    title?: string;
}
export interface IAPIError {
    Message:string ,
    StatusCode:number;
}

export interface ICustomError {
    message:string ,
    severity : 'warning' | 'error';
    code?:string;
}

export const apiErrorInitialize: ICustomError = {
    message: '' ,
    severity: "error",
    code: undefined
  };

