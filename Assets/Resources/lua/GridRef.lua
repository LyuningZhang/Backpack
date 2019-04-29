GridRef=GridRef or BaseClass()

function GridRef:__init(button,image, text)
	self.button=button
	self.butotn.onClick:AddListener(self:UpdateClick)
	self.image=image
	self.text=text
	self.isClick=false
end

function GridRef:__delete()
	self.image=nil
	self.text=nil
	self.gridClick=nil
	self.isClick=nil
end

function GridRef:UpdateClick()
	self.isClick=not self.isClick
	if self.isClick then
		self.image.color=Color.black
	else
		self.iamge.color=Color.white
	end
end