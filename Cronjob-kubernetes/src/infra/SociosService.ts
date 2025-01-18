import { SocioBE, SocioListItem, SocioWrap } from "src/dto/SociosBE";
import { AppConst_Paths } from "./../common/CommonConstants";
import axios from "axios";
import socio_mock from './../mock/socio.json'

export default class SociosService {

    public Search(startModifDate: string, qt: number): Promise<SocioListItem[]> {


        const apiURL = `${AppConst_Paths.ESTADIO_API_URL}/21/qt/${qt}/listsocios`;

        const data = JSON.stringify({
            fromModif: startModifDate//"2023-12-06T18:47:49.627Z",
        });

        const config = {
            method: "post",
            url: apiURL,
            data,
            headers: this.setHeader()
        };

        return axios<SocioListItem[]>(apiURL, config)
            .then((res) => {

                const socios = res.data;
                return socios;
            });

    }

    public get(socioId: number): Promise<SocioBE> {

        //http://192.168.200.6:7076/estadio/21/socio/5233/socio
        const apiURL = `${AppConst_Paths.ESTADIO_API_URL}/21/socio/${socioId}/socio`;

        const config = {
            method: "post",
            url: apiURL,
            Headers: this.setHeader(),
            data: {
                "document": null
            }
        };

        return axios<SocioWrap>(apiURL, config)
            .then((res) => {


                return res.data.socio;
            });

    }

    public getfake(): SocioBE {

        //const socioWrap = res.data as SocioWrap;
        const socioWrap: SocioWrap = JSON.parse(JSON.stringify(socio_mock)) as SocioWrap;

        return socioWrap.socio;


    }
    setHeader() {
        return {
            // crossDomain: "true",
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, OPTIONS",
            "Access-Control-Allow-Headers": "Origin, Content-Type, Authorization, X-Auth-Token",
        };

    };
}
