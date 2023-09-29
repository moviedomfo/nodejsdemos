import {Shape} from "./Shape";

export default class Circle extends Shape {
  radius: number;

  constructor(source?: Circle) {
    super(source);
    if (source) this.radius = source.radius;
  }

  clone(): Shape {
    return new Circle(this);
  }
}
