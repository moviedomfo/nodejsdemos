import {Shape} from "./Shape";

export default class Rectangle extends Shape {
  width: number;
  height: number;

  constructor(source?: Rectangle) {
    super(source);
    if (source) {
      this.width = source.width;
      this.height = source.height;
    }
  }

  clone(): Shape {
    return new Rectangle(this);
  }
}
