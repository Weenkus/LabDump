class SearchPageController < ApplicationController
  def home
  end
  
  def help
  end
  
  def show
  # Check if the users typed anything in the search bar
  if params[:search] != "" 
  
    # Split the search string and remove ""
    values = params[:search].split();
	values.each { |x| x.delete! '"'}
	
	# Set the logical parameters
	if params[:logic] == "and"
		logicParam = " AND "
	end
	if params[:logic] == "or"
	    logicParam = " OR "
	end
	
	# Build the sql string core
	for i in 0..(values.length-1)
		if i != values.length-1
			sqlSubString = sqlSubString.to_s + " text LIKE '%" + values[i].to_s + "%'" + logicParam.to_s
		else
			sqlSubString = sqlSubString.to_s + " text LIKE '%" + values[i].to_s + "%'"
		end
	end
	
    # Exact search
    if params[:method]  == "exact"
		@sql = "SELECT * FROM documents WHERE" + sqlSubString
		@documents = ActiveRecord::Base.connection.execute(@sql).values
	end
  end
  
  render 'home'
  end
  

end
