import 'dotenv/config'
import express from "express";
import path from 'path';
import cors from "cors";
import helmet from "helmet";
import { comerceRouter } from "./route/comercePub.router";
import { personRouter } from "./route/personPub.router";
import morgan from 'morgan';
import { notFoundHandler } from "./common/not-found.middleware";
import { errorHandler } from './common/http-exception';
import swaggerUi from "swagger-ui-express";
// import { RegisterRoutes } from "../build/routes";

require('dotenv').config();

 if (!process.env.PORT) {
    process.exit(1);
 }
 
 
const app = express();


app.set('views',path.join(__dirname,'views'));
app.engine('html', require('ejs').renderFile);
app.set('view engine', 'html');
/**
 *  App Configuration
 */
app.use(helmet());
app.use(cors());
app.use(express.json());


//app.use(morgan('combined'));
//app.use(morgan('tiny'));
app.use(morgan('short'));


// /** Logging */
// itemsRouter.use(morgan('dev'));

// /** Logging */
// authRouter.use(morgan('dev'));

 app.get('/', function (req, res) {
    //res.send('Wellcome to ADN mutiation detector' );
    res.render('index');
  })
  
  app.use(express.static("public"));

  app.use(
    "/docs",
    swaggerUi.serve,
    swaggerUi.setup(undefined, {
      swaggerOptions: {
        url: "/swagger.json",
      },
    })
  );
 
// RegisterRoutes(app);
app.use('/api/comercepubs',comerceRouter);
app.use('/api/personspubs',personRouter);

// Attach the first Error handling Middleware
app.use(notFoundHandler);
app.use(errorHandler);

const PORT = process.env.PORT || 5000;
const URL = `${process.env.BASE_URL}:${PORT}`;

 /**
 * Server Activation
 */
 app.listen(PORT, () => {
    console.log(`App listening on port ${PORT}`);
    console.log(`API url ${URL}`);
    console.log(`API doccumentation ${URL}/docs/`);
    
  });

