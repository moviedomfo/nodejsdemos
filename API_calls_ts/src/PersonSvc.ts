import axios from "axios";
import { AppConst_Paths, AppConstants } from "./CommonConstants";
import { GetCombosRes } from "./Entities/GetCombosRes";
import { IApiResponse } from "./bases";

export class PersonAPIService {


  public GetCombos1 = async (): Promise<GetCombosRes> => {
    const apiURL = `${AppConst_Paths.PERSON_API_URL}/getCombos?&sportClubGuid=${AppConstants.SportClubGuid}`
    let config = {
      method: 'get',
      maxBodyLength: Infinity,
      rejectUnauthorized: false,
      url: apiURL,
      headers: {
        'securityProviderName': 'sportclub'
      }
    };
    return axios.get<GetCombosRes>(apiURL, config)
      .then((res) => {

        return res.data;
      });


  };



  public GetCombos2 = async () => {
    const apiURL = `${AppConst_Paths.PERSON_API_URL}/getCombos?&sportClubGuid=${AppConstants.SportClubGuid}`
    let config = {
      method: 'get',
      maxBodyLength: Infinity,
      rejectUnauthorized: false,
      url: apiURL,
      headers: {
        'securityProviderName': 'sportclub'
      }
    };


    return new Promise<GetCombosRes>((resolve, reject) => {
      return axios.get<GetCombosRes>(apiURL, config)
        .then((res) => {

          resolve(res.data);
        })
        .catch((err) => reject(err));
    });
  };

}


