
import dayjs from "dayjs";
import utc from "dayjs/plugin/utc";
dayjs.extend(utc);


//"2024-06-01T21:56:03-03:00"

/**
 * 
 */
export class DateDemos {

    /**
     * 
     * @param startDate 
     */
    public static ParceUTC(startDate: string) {
        const startDateA = new Date(startDate); 							//Sat Jun 01 2024 21:56:03 GMT-0300 (hora estándar de Argentina)
        const startDateA_ToString = startDateA.toString();					//Sat Jun 01 2024 21:56:03 GMT-0300 (hora estándar de Argentina)
        const startDateA_Format_utc = dayjs(startDateA).utc().format();	//"2024-06-02T00:56:03Z"
        const startDateA_Format = dayjs(startDate).format();
        const startDateParsed = dayjs.utc(startDate).toDate(); // "2024-06-01T21:56:03.257Z"

        console.log(`startDateA = new Date(startDate)      ${startDateA}`);
        console.log(`startDateA.toString()                 ${startDateA_ToString}`);
        console.log(`dayjs(startDate).utc().format()       ${startDateA_Format_utc}`);
        console.log(`dayjs(startDate).format()             ${startDateA_Format}`);

        console.log(`dayjs.utc(startDate).toDate()         ${startDateParsed}`);
    }

}