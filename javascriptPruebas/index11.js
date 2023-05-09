
/// Funcion Sort Array
var items = [
    { name: 'Edward', value: 21 },
    { name: 'Sharpe', value: 37 },
    { name: 'And', value: 45 },
    { name: 'The', value: -12 },
    { name: 'Magnetic', value: 13 },
    { name: 'Zeros', value: 37 }
  ];


items.sort( (a, b) => {
    if (a.name > b.name) {
      return 1;
    }
    if (a.name < b.name) {
      return -1;
    }
    // a must be equal to b
    return 0;
  });

   console.log (items);
  const item = items.find(p=> p.name > 'Ma%' );
  
  
  console.log (item);

  const itemFiltereds = items.filter(p=> p.name > 'Ma%' );
  console.log (itemFiltereds);