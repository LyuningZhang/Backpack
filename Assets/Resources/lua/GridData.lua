GridData=GridData or BaseClass()

function GridData:__init(id, amount)
	self.amount=amount
	self.id=id
end

function GridData:__delete()
	self.amount=nil
	self.id=nil
end

function GridData:Update(id)
	self.id=id
	self.amount=1
end

function GridData:Clear()
	self.amount=0
	self.id=0
end