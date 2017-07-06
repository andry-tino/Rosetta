class MyClass {
  private member1 : number;

  public Method1() : void {
    if (true)
    {
      var initVariable1 : string = 'Hello';
    }
  }
  private Method2() : void {
    if (true)
    {
      var initVariable1 : string = 'Hello';
    }
    else if (false)
    {
      var initVariable2 : string = 'Hello';
    }
    else
    {
      var initVariable3 : string = 'Hello';
    }
  }
  public Method3() : boolean {
    return true;
  }
  public Method4() : boolean {
    return false;
  }
  public Method5() : void {
    throw null;
  }
  public Method6() : void {
    this.member1 = 1;
  }
}