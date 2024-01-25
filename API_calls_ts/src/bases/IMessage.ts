

export interface IMessage{
    title: string,
    text:string ,
    severity : 'success' | 'info' | 'warning' | 'error';

}


export interface ICrudMessage{
    Result: string,
    Message:string ,
    Id:string

}