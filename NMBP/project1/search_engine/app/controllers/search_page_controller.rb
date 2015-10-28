class SearchPageController < ApplicationController
  def home
  end
  
  def help
  end
  
  def anal
  end
  
  def analQuery
  @d1 = params[:start_date]["year"]
  @d2 = params[:end_date]
  
  
    @results = Record.all
    
    #sqlRecords = "SELECT search FROM records WHERE YEAR(time) >= " + params[:start_date]["year"]
    #@results = ActiveRecord::Base.connection.execute(sqlRecords).values
    
    #sqlRecords = "SELECT * FROM records WHERE CAST(time AS date) BETWEEN '" 
    #+ params[:start_date]["year"] + "/" + params[:start_date]["month"] + "/" + params[:start_date]["day"] + "' 
    #and '" + params[:end_date]["year"] + "/" + params[:end_date]["month"]+ "/" + params[:end_date]["day"] + "'"
    
    
    #@results = ActiveRecord::Base.connection.execute(sqlRecords).values
    #@results = Record.where("created_at.date >= :start_date AND created_at.date<= :end_date",
    #{start_date: params[:start_date], end_date: params[:end_date]})
    
    
    #@results = Record.where('DATE(time.date) BETWEEN ? AND ?', params[:start_date], params[:end_date]).first
   
    
   #@results = Record.where("created_at >= :start_date AND created_at <= :end_date",
	#{start_date: params[:start_date], end_date: params[:end_date]})

    #for i in 0..(@records.length-1)
		#if !((@records[i].time.date >= params[:start_date].date) && (@records[i].time <= params[:end_date]))
			#@records.delete_at(i)
		#end
	#end
    

    # Fatch the user selected data
    if params[:time] == "month"

	elsif params[:time] == "day"
		#@results = Record.where("time.date >= :start_date AND time.date <= :end_date",
		#{start_date: params[:start_date], end_date: params[:end_date]})
	end
	
  # Render the search results to the user
  render 'anal'
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
		logicSign = " & "
	end
	if params[:logic] == "or"
	    logicParam = " OR "
	    logicSign = " | "
	end
	
	# Add result text bolding 
	if params[:method]  == "exact" || params[:method]  == "dictionaries" || params[:method]  == "fuzzy"
		rankHeadlineString = "ts_headline('english', text, to_tsquery('"
		for i in 0..(values.length-1)
			if i != values.length-1
				rankHeadlineString = rankHeadlineString.to_s + "(" + values[i].to_s.gsub(" ", " & ") + ")" + logicSign.to_s
			else
				rankHeadlineString = rankHeadlineString.to_s + "(" + values[i].to_s.gsub(" ", " & ") + ")'))";
			end
		end
	end
	
	# Add result ranking
	if params[:method]  == "exact" || params[:method]  == "dictionaries" || params[:method]  == "fuzzy"
		rankHeadlineString = rankHeadlineString + ", ts_rank(texttsv, to_tsquery('"
		for i in 0..(values.length-1)
			if i != values.length-1
				rankHeadlineString = rankHeadlineString.to_s + "(" + values[i].to_s.gsub(" ", " & ") + ")" + logicSign.to_s
			else
				rankHeadlineString = rankHeadlineString.to_s + "(" + values[i].to_s.gsub(" ", " & ") + ")')) AS rank";
			end
		end
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
				sqlSubString = sqlSubString.to_s + "(" + values[i].to_s.gsub(" ", " & ") + ")" + logicSign.to_s
			else
				sqlSubString = sqlSubString.to_s + " text % '" + values[i].to_s + "'"
			end
		end
	end
	
	
    # Search with the constructed SQL string
    if params[:method]  == "exact" || params[:method]  == "dictionaries" || params[:method]  == "fuzzy"
		@sql = "SELECT text, " + rankHeadlineString + " FROM documents WHERE" + sqlSubString + " ORDER BY rank DESC"
		@documents = ActiveRecord::Base.connection.execute(@sql).values
	end
	
	# Before rendering the result, save the search query inside the DB
	for i in 0..(values.length-1)
		if i != values.length-1
			recordSave = recordSave.to_s + "'" + values[i].to_s.gsub(" ", " & ") + "'" + logicSign.to_s
		else
			recordSave = recordSave.to_s + "'" + values[i].to_s.gsub(" ", " & ") + "'";
		end
	end
	
	# Update/create the record inside the DB
	record = Record.create(search: recordSave.to_s, count: 1, time: DateTime.now)
	#record = Record.find_by(search: recordSave.to_s)
	#if record.nil?
		#record = Record.create(search: recordSave.to_s, count: 0, time: DateTime.now)
	#else
		#record.count = record.count + 1
		#record.save
	#end
  end

  
  # Render the search results to the user
  render 'home'
  end
end
