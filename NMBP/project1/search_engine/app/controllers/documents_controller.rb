class DocumentsController < ApplicationController
  def index
    @documents = Document.all
  end

  def new
  end
	
  def create
    @document = Document.new(document_params)
    @document.save
    redirect_to action: "index"
  end
  
  private
    def document_params
	  params.require(:document).permit(:text)
	end
	
end
