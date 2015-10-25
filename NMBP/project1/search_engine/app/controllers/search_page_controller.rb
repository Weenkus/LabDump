class SearchPageController < ApplicationController
  def home
  end
  
  def help
  end
  
  def show
  # Check if the users typed anything in the search bar
  if params[:search] != "" 
  
    # Split the search string and remove ""
    values = params[:search].split("\" \"");
	values.each { |x| x.delete! '"'}
	
	# Set the logical parameters
	if params[:logic] == "and"
		logicParam = " AND "
	end
	if params[:logic] == "or"
	    logicParam = " OR "
	end
	
	# Build the sql string core
	if params[:method]  == "exact"
		for i in 0..(values.length-1)
			if i != values.length-1
				sqlSubString = sqlSubString.to_s + " text LIKE '%" + values[i].to_s + "%'" + logicParam.to_s
			else
				sqlSubString = sqlSubString.to_s + " text LIKE '%" + values[i].to_s + "%'"
			end
		end
	elsif params[:method]  == "dictionaries"
		for i in 0..(values.length-1)
			if i != values.length-1
				sqlSubString = sqlSubString.to_s + " texttsv @@ '" + values[i].to_s.gsub(" ", " & ") + "'::TSQuery" + logicParam.to_s
			else
				sqlSubString = sqlSubString.to_s + " texttsv @@ '" + values[i].to_s.gsub(" ", " & ") + "'::TSQuery"
			end
		end
	elsif params[:method]  == "fuzzy"
		for i in 0..(values.length-1)
			if i != values.length-1
				sqlSubString = sqlSubString.to_s + " text % '" + values[i].to_s + "'" + logicParam.to_s
			else
				sqlSubString = sqlSubString.to_s + " text % '" + values[i].to_s + "'"
			end
		end
	end
	
	
    # Exact search
    if params[:method]  == "exact"
		@sql = "SELECT * FROM documents WHERE" + sqlSubString
	elsif params[:method]  == "dictionaries"
		@sql = "SELECT * FROM documents WHERE" + sqlSubString
	elsif params[:method]  == "fuzzy"
		@sql = "SELECT * FROM documents WHERE" + sqlSubString
	end
	@documents = ActiveRecord::Base.connection.execute(@sql).values
	
  end
  
  render 'home'
  end
  

end
