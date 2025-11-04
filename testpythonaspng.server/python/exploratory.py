import pandas as pd
import numpy as np
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
import seaborn as sns

sns.set()

def example_return_columns_dict(filename: str)  -> list[dict[str,str]]:
    return [{"data": "AAA", "title": "AAA"}, {"data": "BBB", "title": "BBB"}] 

def get_columns(filename: str)  -> list[dict[str,str]]:
    explor_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
    explor_columns = explor_df.columns
    dtable_columns = []
    for column in explor_columns:
        dtable_columns.append({"data": column, "title": column})
    return  dtable_columns

def get_columns_type(filename: str)  -> list[dict[str, str]]:
    explor_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
    explor_columns = explor_df.columns
    dtable_columns = []
    for column in explor_columns:
        dtable_columns.append({"column": column, "type": str(explor_df[column].dtype)})
    return  dtable_columns

def get_corralation(filename: str) -> str:
	#model_df = pd.read_csv(file_path + '\\product_vending_analisys.csv', encoding='cp1251',  sep=',')
	model_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
	df = model_df.copy()
	df = df[df['MAGAZZINO'] == 'BAZA']
	#df.CO1_PEK = df.CO1_PEK.str.replace(',','.').astype(float)
	df_train = df[df.COG_DAT<='2024-12-31']
	df_test = df[df.COG_DAT>='2025-01-01']
	df_train = df_train.drop(['MAGAZZINO','COG_DAT'], axis=1).reset_index(drop=True)

	#print(df_train.head())

	corrmat = df_train.corr()
	top_corr_features = corrmat.index
	plt.figure(figsize=(10,10))
	# Plot heat map
	
	g=sns.heatmap(df_train[top_corr_features].corr(),annot=True,cmap="coolwarm",vmin=-1,vmax=1,center=0)

	
	#plt.savefig('wwwroot/corelation_heatmap.png');
	plt.savefig('corelation_heatmap.png');

	#print("after sns.heatmap")

	return 'corelation_heatmap.png';







