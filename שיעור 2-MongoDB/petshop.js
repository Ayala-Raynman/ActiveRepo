use petshop
db.pets.insert({name: "Davey Bungooligan", species: "Piranha"})
db.pets.insert({name: "Suzy B", species: "Cat"})
db.pets.insert({name: "Mikey", species: "Hotdog"})
db.pets.insert({name: "Terrence", species: "Sausagedog"})
db.pets.insert({name: "Philomena Jones", species: "Cat"})
//1
db.pets.insert({name: "Henry", species: " Pirahna"})
db.pets.insert({name: "Henry", species: " Rat"})
//2
db.pets.find({name :"Mikey",species:"Gerbil"},{_id:true});
//3
db.pets.find({_id:ObjectId("60e32ee06aaa7dbfc7103241")})
//4
db.pets.find({species:"Gerbil"})
//5
db.pets.find({name:"Mikey"})
//6
db.pets.find({species:/dog/})
//7
db.pets.find({name :"Mikey",species:"Gerbil"});