export class PersonBE {
    PersonId: number;
    UserId: string | null;
    DocType: number;
    DocNumber: string;
    Lastname: string;
    Name: string;
    Sex: number;
    CreatedDate: Date | null;
    DateOfBirth: Date;
    Photo: Uint8Array | null;
    IsUserActive: boolean;
    Street: string;
    StreetNumber: number | null;
    Floor: string;
    CountryId: number | null;
    ProvinceId: number | null;
    CityId: number | null;
    Neighborhood: string;
    Mail: string | null;
    Phone1: string;
    Phone2: string;
    Province: string;
    City: string;
    ZipCode: string;
    LastAccessTime: Date | null;
    LastAccessUserId: string | null;
    IsMember: boolean;
    FamilyGroupId: number | null;
    FamilyGroupMemberType: number;
    MaritalStatusId: number;
    MemberState: number | null;
    DischargeDate: Date | null;
    MemberNumber: number | null;

    // static GetFullName(lastName: string, name: string): string {
    //     if (!lastName || lastName.trim() === "") {
    //         lastName = "";
    //     }
    //     if (!name || name.trim() === "") {
    //         name = "";
    //     }

    //     return `${lastName.trim()}, ${name.trim()}`.trim();
    // }
}
