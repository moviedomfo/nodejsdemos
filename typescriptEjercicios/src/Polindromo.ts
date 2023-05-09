/**
 *
 * @param prhase
 */
const IsPalindrome = (prhase: string): boolean => {
  const strA = prhase.replace(/[ ,]/g, "").toLowerCase();
  // [...strA] transform in array
  const strB = [...strA].reverse().join("");

  //let IsPalindrome = true;
  // [...strA].forEach((item, index) => {
  //   IsPalindrome = item === strB[index];
  //   if (!IsPalindrome) { 
  //     return;
  //   }
  // });
  // return IsPalindrome;

  return strA === strB;
};

export default IsPalindrome;
