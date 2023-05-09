use sportmanager
--select * from [dbo].[Persons]
--where[dbo].[Persons].GeneratedDate is not null

--select count(CloudId),CloudId[count] from[dbo].[Persons]
--where topic = 'apple'
--group by CloudId

select top 1000 * from [dbo].[Persons]
order by PersonId desc

--where CloudId = '021cf927-13d6-48a7-baef-f0bf078916aa'
--delete from [dbo].[Persons] where topic = 'orange'