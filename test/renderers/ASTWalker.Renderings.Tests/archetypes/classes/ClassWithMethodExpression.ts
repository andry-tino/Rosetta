class MyClass {
  public constructor() {
    var initVariable1 : string = 'Hello';
  }

  public Method1() : void {
    var initVariable1 : string = 'Hello';
  }
  Method2() : void {
    var initVariable1 : int = 1 + 4;
    var initVariable2 : int = 2 * 4 + (3 / 2);
  }
  private Method3() : void {
    var initVariable1 : bool = !false;
    var initVariable2 : bool = !true;
  }
  private Method4() : void {
    var initVariable1 : int = 1++;
    var initVariable2 : int = ++1;
    var initVariable1 : int = 1--;
    var initVariable2 : int = --1;
  }
  private Method5() : void {
    var initVariable1 : int = (1);
  }
  Method6() : void {
    var initVariable1 : bool = true == false;
    var initVariable2 : bool = true != false;
    var initVariable3 : bool = 1 == 2;
    var initVariable4 : bool = 1 != 2;
    var initVariable5 : bool = 'hello' == 'Hello';
    var initVariable6 : bool = 'hello' != 'Hello';
  }
  private Method7() : void {
    var initVariable1 : bool = true;
    initVariable1 = false;
    var initVariable2 : int = 1;
    initVariable2 = 0;
    var initVariable3 : string = 'hello';
    initVariable3 = 'hello!';
  }
}