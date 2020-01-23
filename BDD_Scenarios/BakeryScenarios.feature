Feature: BakeryOwner
	US 1. As a bakery owner, I can add a bakery good to let the customers see what is currenlty fresh in sell. 
	Conditions of satisfaction: Can I store product’s name and price?
	US 2. As a bakery customer, I can view all the fresh bakery goods in sell to step in. 
	Conditions of satisfaction: Can I see product’s price?
	US 3. As a bakery customer, I can check what is the most fresh bakery good right now in sell.

Background:
	Given Bakery goods are prepared for viewing

@userStory1
Scenario: Owner adds new bakery good
	Given Owner has baked new product 'Roll' that costs '1.5'
	Then Customer can buy new product

@userStory1
Scenario: Owner adds new bakery good that already exists
	Given Owner has baked product that already exists
	Then Customer can buy new product

@userStory1
Scenario: Owner adds new bakery good without price
	Given Owner has baked product 'Cake' without price
	Then Customer cannot buy new product without price

@userStory1
Scenario: Owner adds new bakery good without name
	Given Owner has baked new product with price '1.6' and without name
	Then Customer cannot buy new product without name

@userStory1
Scenario: Owner adds new bakery good without name and price
	Given Owner has baked new product without name and price
	Then Customer cannot buy new product without name and price

@userStory2
Scenario: Customer checks all fresh bakery goods
	When Customer asks for all bakery goods
	Then All bakery goods are visible for customer

@userStory3
Scenario: Customer asks for most fresh bakery good
	Given Owner has baked new product 'Cheese cake' that costs '2.1'
	When Customer asks for most fresh bakery good
	Then Bakery good with name 'Cheese cake' and price '2.1' is visible is visible
