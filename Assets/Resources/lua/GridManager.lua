GridManager=GridManager or BaseClass()

function GridManager:__init(transform)
	for i=1,transform.childCount do
		local button=transform:GetChild(i-1):GetComponent("Button")
		local image=transform:GetChild(i-1):GetComponent("Image")
		local text=transform:GetChild(i-1):GetChild(0):GetComponent("Text")
		self.gridRefList[i]=GridRef:New(button,image,text)
		self.gridDataList[i]=GridData:New(0,0)
	end
end

function GridManager:_delete()
	self.gridRefList=nil
	self.gridDataList=nil
end

function GridManager:Add()
	local temp=math.random(1,3)
	local isExist=false
	local isEmpty=false
	local firstEmpty=-1
	local existId=-1
	for i=1,#self.gridDataList do
		if self.gridDataList[i].id == temp then
			isExist=true
			if existId < 0 then
				existId=i
			else
				if self.gridDataList[i].amount < 9 then
					existId=i
				end
			end
		end
		if isEmpty == false then
			if self.gridDataList[i].amount <= 0 then
				firstEmpty=i
				isEmpty=true
			end
		end
		if isEmpty == false and isExist == false and i == #self.gridDataList then
			return false
		end
	end
	if isExist then
		if self.gridDataList[existId].amount < 9 then
			self.gridDataList[existId].amount=self.gridDataList[existId].amount+1
			self.gridRefList[existId].text.text=self.gridDataList[existId].amount
		else
			if firstEmpty == -1 then
				print("Backpack is full")
				return false
			end
			self.gridDataList[firstEmpty]:Update(temp)
		end
	else
		if firstEmpty == -1 then
			print("Backpack is full")
			return false
		end
		self.gridDataList[firstEmpty]:Update(temp)
	end
	FileOperator:Save(serialize(self.gridDataList))
	return true
end

function GridManager:Delete()
	print("This is delete event")
	idList={}
	for i=1,#self.gridRefList do
		if self.gridRefList[i].gridClick.isClick then
			idList[#idList+1]=i
		end
	end
	if #idList <= 0 then
		print("delete finished")
		return false
	end
	for i=1,#idList do
		self.gridDataList[idList[i]]:Clear()
		self.gridRefList[idList[i]]:UpdateClick()
	end
	FileOperator:Save(serialize(self.gridDataList))
	return true
end

function GridManager:Sort()
	table.sort( self.gridDataList, 
		function (v1,v2) 
		 	if v1.id ~= v2.id then
		 		return v1.id < v2.id
		 	else
		 		return v1.amount < v2.amount
		 	end
		 end)
	for i=1,#self.gridDataList do
		if self.gridDataList[i].amount > 0 then
			self.gridRefList[i].image.sprite=class.sprite[self.gridDataList[i].id + 1]
			self.gridRefList[i].text.text=self.gridDataList[i].amount
		else
			self.gridRefList[i].image.sprite=class.sprite[1]
			self.gridRefList[i].text.text=""
		end
	end
	FileOperator:Save(serialize(self.gridDataList))
end
