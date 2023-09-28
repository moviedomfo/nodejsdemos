export abstract class Shape {
  x: number;
  y: number;
  color: string


  constructor(source?: Shape) {
    this.x = source.x;
    this.x = source.x;
    this.color = source.color;
  }
  abstract clone(): Shape;
}
