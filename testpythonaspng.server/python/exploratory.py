import pandas as pd
import numpy as np
import matplotlib
matplotlib.use('Agg')
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error

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

	
	plt.savefig('wwwroot/corelation_heatmap.png');
	#plt.savefig('corelation_heatmap.png');

	#print("after sns.heatmap")

	return 'corelation_heatmap.png';

def get_corralation_per_week(filename: str) -> str:

	model_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
	#vending_product_date_df = vending_product_date_df.drop(['Unnamed: 0'], axis=1).reset_index(drop=True)
	model_df = model_df.reset_index(drop=True)
	model_df['COG_DAT'] = pd.to_datetime(model_df['COG_DAT'])

	df = model_df.copy()
	df_train = df[df['COG_DAT'] <= pd.to_datetime('15-07-2024')]
	df_train = df_train[df['CO1_NEC'] > 0]
	df_train = df_train[df['MAGAZZINO'] == 'BAZA']
	df_train['COST'] = df_train['CO1_NEC'] * df['CO1_PEK']
	df_trian = df_train.reset_index(drop=True)

	week_grouped_df_train = (df_train.groupby(['Week'], as_index=False)
                              .agg({'CO1_NEC': 'sum',
                                    'COST': 'sum',
                                    'UPV_COD': 'count'}))
	week_grouped_df_train['CO1_PEK'] = week_grouped_df_train.COST / week_grouped_df_train.CO1_NEC

	df_train = week_grouped_df_train
	# === correlation ===
	corrmat = df_train.corr()
	#====================
	top_corr_features = corrmat.index
	plt.figure(figsize=(10,10))
	# # Plot heat map
	g=sns.heatmap(df_train[top_corr_features].corr(),annot=True,cmap="coolwarm",vmin=-1,vmax=1,center=0)

	plt.savefig('wwwroot/corelation_heatmap_per_week.png');
#	plt.savefig('ClientApp/corelation_heatmap_per_week.png');

	return 'corelation_heatmap_per_week.png';

def get_mlr_evaluation(filename: str) -> list[dict[str,str]]:
	model_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
	model_df = model_df.reset_index(drop=True)
	model_df['COG_DAT'] = pd.to_datetime(model_df['COG_DAT'])

	df = model_df.copy()
	df_train = df[df['COG_DAT'] <= pd.to_datetime('15-07-2024')]
	df_train = df_train[df['CO1_NEC'] > 0]
	df_train = df_train[df['MAGAZZINO'] == 'BAZA']
	df_train['COST'] = df_train['CO1_NEC'] * df['CO1_PEK']
	df_trian = df_train.reset_index(drop=True)

	week_grouped_df_train = (df_train.groupby(['Week'], as_index=False)
                              .agg({'CO1_NEC': 'sum',
                                    'COST': 'sum',
                                    'UPV_COD': 'count'}))
	week_grouped_df_train['CO1_PEK'] = week_grouped_df_train.COST / week_grouped_df_train.CO1_NEC

	df_train = week_grouped_df_train
	X_train = df_train[['UPV_COD','CO1_PEK', 'Week']]
	y_train = df_train.CO1_NEC

	
	reg = LinearRegression()
	reg.fit(X_train, y_train)
	y_train_predict = reg.predict(X_train)
	
	reg_intercept = reg.intercept_
	independed_predicts = ['UPV_COD', 'CO1_PEK', 'Week']
	coeficients = reg.coef_
	r_square = reg.score(X_train, y_train)*100
	mean_square_err = mean_squared_error(df_train['CO1_NEC'], y_train_predict)

	evaluation_stats = []
	evaluation_stats.append({'intercept': str(round(reg_intercept))})
	for i  in  range(len(coeficients)) :
		evaluation_stats.append({independed_predicts[i]: str(round(coeficients[i], 2))})
		    
	evaluation_stats.append({'r_square': str(round(r_square))})
	evaluation_stats.append({'mean_square_error': str(round(mean_square_err))})

	return evaluation_stats 












