// retorna un array de una enumeracion

const CategoryEnum  = [
    { name: 'Edward', value: 21 },
    { name: 'Sharpe', value: 37 },
    { name: 'And', value: 45 },
    { name: 'The', value: -12 },
    { name: 'Magnetic', value: 13 },
    { name: 'Zeros', value: 37 }
  ];
  
  const seasons = {
    SUMMER: "summer",
    WINTER: "winter",
    SPRING: "spring",
    AUTUMN: "autumn",
  }
  let seasonsArray = Object.keys(seasons).map(key=>{
    return seasons[key];
  })


  let xx = Object.keys(CategoryEnum).map(key=>  CategoryEnum[key]);

    console.log (seasonsArray);
