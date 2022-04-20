
export  class PersonagemModel{
  public nome: string;
  public descricao: string;
  public urlImage: string;
  public eventos: Array<Evento> = new Array();
}

export class Evento{
  public ImagePersonContent: string;
  public Titulo: string;
  public Descricao: string;
}
