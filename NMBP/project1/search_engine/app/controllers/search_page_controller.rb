class SearchPageController < ApplicationController
  def home
  end
  
  def help
  end
  
  def show

  # Check if the users typed anything in the search bar
  if params[:search] != "" 
    # Exact search
    if params[:method]  == "exact"
		sqlString = "select * from documents where text like '%" + params[:search].to_s + "%'";
		@documents = ActiveRecord::Base.connection.execute(sqlString).values
	end
  end
  
  render 'home'
  end
  

end
