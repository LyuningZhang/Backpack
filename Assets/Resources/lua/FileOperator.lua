FileOperator=FileOperator or BaseClass()

function FileOperator:__init()
end

function FileOperator:__delete()
end

function FileOperator:Save(str)
	if UserFunction.Save(str) then
		print("save succeed")
	else
		print("save failed")
	end
end

function FileOperator:Load()
	str=UserFunction.Load()
	if str ~= "" then
		return unserialize(str)
    else
        print("data is nil")
        return ""
	end
end

function serialize(_t)
    local szRet = "{"
    function doT2S(_i, _v)
        if "number" == type(_i) then
            szRet = szRet .. "[" .. _i .. "] = "
            if "number" == type(_v) then
                szRet = szRet .. _v .. ","
            elseif "string" == type(_v) then
                szRet = szRet .. '"' .. _v .. '"' .. ","
            elseif "table" == type(_v) then
                szRet = szRet .. serialize(_v) .. ","
            else
                szRet = szRet .. "nil,"
            end
        elseif "string" == type(_i) then
            szRet = szRet .. '["' .. _i .. '"] = '
            if "number" == type(_v) then
                szRet = szRet .. _v .. ","
            elseif "string" == type(_v) then
                szRet = szRet .. '"' .. _v .. '"' .. ","
            elseif "table" == type(_v) then
                szRet = szRet .. serialize(_v) .. ","
            else
                szRet = szRet .. "nil,"
            end
        end
    end
    table.foreach(_t, doT2S)
    szRet = szRet .. "}"
    return szRet
end

function unserialize(str)
    str = "return " .. str;
    local fun = loadstring(str);
    return fun();
end