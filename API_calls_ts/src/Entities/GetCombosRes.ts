import { CityBE } from "./CityBE";
import { MemberStateBE } from "./MemberStateBE";
import { ParamBE } from "./ParamBE";
import { ProvinceBE } from "./ProvinceBE";

export class GetCombosRes {
  DocTypeList: ParamBE[];
  EstadoCivilList: ParamBE[];
  SexList: ParamBE[];
  BlodType: ParamBE[];

  Provinces: ProvinceBE[];
  Cities: CityBE[];
  MemberStates: MemberStateBE[];
}
