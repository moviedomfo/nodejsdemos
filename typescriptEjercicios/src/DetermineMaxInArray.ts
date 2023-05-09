
/**
 * 
 * @param numbers 
 */
 const DetermineMaxInArray =(numbers:number[]):any => {

    //The return value of the callback function (in the reduce ) is the accumulated result, and is provided as an argument 
    // in the next call to the callback function.
    const max = numbers.reduce((prev,current) =>{
        return Math.max(prev,current); 
    },0);
    //prev init in 0 
    return max;
    
//        current%2 !==0 ? current
}

export default DetermineMaxInArray;