

const vowels = ['a', 'e', 'i', 'o', 'u'];
const countVowels = (phrase) => {



  const prhaseToaray = phrase.toLowerCase().split('');
  const count = prhaseToaray.reduce((acc, item) => {

    //return o la suma o el mismo acc
    return vowels.includes(item) ? acc + 1 : acc;
 
    // if (!vowels.includes(item))
    //   acc++;

  }, 0);

  return count;
}

const c = countVowels('holaque tal');

console.log(c);

