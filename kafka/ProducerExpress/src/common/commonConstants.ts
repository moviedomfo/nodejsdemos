
/**
 * Common constats
 */
export  const AppConstants = {
    
     Verion: process.env.APP_VERSION as string  ,
     COMPANY: 'Pelsoft',
     ClientId:process.env.KAFKA_CLIENT_ID || 'Pelsoft API Express' ,
     Brokers: process.env.KAFKA_BROKERS.split(',') || [],
     BD_SERVER_URI:process.env.BD_SERVER_URI,
     BD_SERVER_PWD:process.env.BD_SERVER_PWD,
     BD_SERVER_USER:process.env.BD_SERVER_USER,

     R_EXP_MORE_THAN_4CHAR_CONTINUOS:` *\\b(?=[a-z\\d]*([a-z\\d])\\1{3}|\\d+\\b)[a-z\\d]+`,
     
     
};


