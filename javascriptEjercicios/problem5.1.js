let str = 'Yo dono rosas, oro no doy';

const isPalindrome1 = (str) => {
  //regular exp en replace :[chars to replace]
  let strA = str.replace(/[ ,]/g, '').toLowerCase();

  let strB = [...strA].reverse().join('');
  for (let i = 0; i < strA.length - 1; i++) {
    if (strA[i] !== strB[i])
      return false
  }
  return true;
}

// menos eficiente
const isPalindrome2 = (str) => {

  let strA = str.split(' ').join('').replace(/,/g, '').toLowerCase();
  let strB = [...strA].reverse().join('');
  // console.log(strA);
  // console.log(strB);
  for (let i = 0; i < strA.length - 1; i++) {

    if (strA[i] !== strB[i])
      return false

  }

  return true;
}

console.log(isPalindrome1(str));
