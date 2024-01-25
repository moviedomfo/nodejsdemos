
export interface ParamBE {
    Id: number;
    ParamId: number;
    Culture: string;
    Name: string;
    ParamTypeId: number | null;
    ParentId: number | null;
    Description: string;
    Enabled: boolean;
    UserId: string | null;
}